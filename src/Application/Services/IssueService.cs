using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepo;
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly IAuthorizationService _auth;

        public IssueService(
            IIssueRepository issueRepository,
            IUserService userService,
            IProjectService projectService,
            IAuthorizationService authorizationService)
        {
            _issueRepo = issueRepository;
            _userService = userService;
            _projectService = projectService;
            _auth = authorizationService;
        }

        public void CreateIssue(
            string title,
            string description,
            int priority,
            DateTime dueDate,
            Guid managerId,
            Guid projectId)
        {
            var newIssue = new Issue(title, description, (IssuePriority)priority, dueDate, managerId, projectId);
            _issueRepo.Add(newIssue);
        }

        public void AssignManager(Guid issueId, Guid newManagerId, User currentUser)
        {
            var issue = GetById(issueId);   // Ensure the issue exists
            _userService.GetById(newManagerId);    // Ensure the new manager exists

            _auth.EnsureCanManageIssue(issue, currentUser);

            issue.AssignManager(newManagerId);
            _issueRepo.Update(issue);
        }

        public void AssignIssue(Guid issueId, string developerEmail, User currentUser)
        {
            var issue = GetById(issueId);   // Ensure the issue exists
            var developer = _userService.GetByEmail(developerEmail);    // Ensure the developer exists

            _auth.EnsureCanManageIssue(issue, currentUser);

            issue.AssignTo(developer.Id);
            _issueRepo.Update(issue);
        }

        public void UnassignIssue(Guid issueId, User currentUser)
        {
            var issue = GetById(issueId);   // Ensure the issue exists

            _auth.EnsureCanManageIssue(issue, currentUser);

            issue.Unassign();
            _issueRepo.Update(issue);
        }

        public void ChangeStatus(Guid issueId, int newStatus, User currentUser)
        {
            var issue = GetById(issueId);   // Ensure the issue exists

            _auth.EnsureCanChangeIssueStatus(issue, currentUser);

            issue.ChangeStatus((IssueStatus)newStatus);
            _issueRepo.Update(issue);
        }

        public void ChangeDuedate(Guid issueId, DateTime newDuedate, User currentUser)
        {
            var issue = GetById(issueId);   // Ensure the issue exists

            _auth.EnsureCanManageIssue(issue, currentUser);

            issue.ChangeDueDate(newDuedate);
            _issueRepo.Update(issue);
        }

        public void CloseIssue(Guid issueId, User currentUser)
        {
            var issue = GetById(issueId);   // Ensure the issue exists

            _auth.EnsureCanManageIssue(issue, currentUser);

            issue.ChangeStatus(IssueStatus.Closed);
            _issueRepo.Update(issue);
        }

        public void DeleteIssue(Guid issueId, User currentUser)
        {
            var issue = GetById(issueId);   // Ensure the issue exists

            _auth.EnsureCanManageIssue(issue, currentUser);

            _issueRepo.Remove(issueId);
        }

        public Issue GetById(Guid issueId)
            => _issueRepo.GetById(issueId) ?? throw new InvalidOperationException("Issue not found");

        public IEnumerable<Issue> GetByProject(Guid projectId, User currentUser)
        {
            var project = _projectService.GetById(projectId);   // Ensure the project exists

            _auth.EnsureCanViewProjectIssues(project, currentUser);

            return _issueRepo.GetByProject(projectId);
        }

        public IEnumerable<Issue> GetByManager(Guid managerId)
            => _issueRepo.GetByManager(managerId);

        public IEnumerable<Issue> GetByDeveloper(Guid developerId)
            => _issueRepo.GetByDeveloper(developerId);

        public IDictionary<string, IEnumerable<Issue>> GetForManager(Guid managerId)
        {
            var managerIssues = _issueRepo.GetByManager(managerId);
            var managerProjects = _projectService
                .GetProjectsManagedBy(managerId)
                .Select(proj => new { proj.Id, proj.Name });

            var managerIssuesGroupedByProjectNames = managerProjects.ToDictionary(
                proj => proj.Name,
                proj => managerIssues.Where(issue => issue.ProjectId == proj.Id)
            );

            return managerIssuesGroupedByProjectNames;
        }

        public IDictionary<string, IEnumerable<Issue>> GetByAssignee(Guid managerId)
        {
            var managerIssues = _issueRepo.GetByManager(managerId);
            var developers = _userService.GetDevelopers();

            var managerIssuesGroupedByDeveloperEmails = developers.GroupJoin(
                managerIssues,
                dev => dev.Id,
                issue => issue.AssigneeId,
                (dev, issues) => new { DeveloperEmail = dev.Email, Issues = issues }
            ).ToDictionary(
                group => group.DeveloperEmail,
                group => group.Issues
            );

            return managerIssuesGroupedByDeveloperEmails;
        }

        public IDictionary<string, IEnumerable<Issue>> GetForDeveloper(Guid developerId)
        {
            var developerIssues = _issueRepo.GetByDeveloper(developerId);
            var projects = _projectService.GetAllProjects();

            var developerIssuesGroupedByProjectNames = projects.GroupJoin(
                developerIssues,
                proj => proj.Id,
                issue => issue.ProjectId,
                (proj, issues) => new { ProjectName = proj.Name, Issues = issues }
            ).ToDictionary(
                group => group.ProjectName,
                group => group.Issues
            );

            return developerIssuesGroupedByProjectNames;
        }

        public IDictionary<string, IEnumerable<Issue>> GetByStatus(Guid managerId)
            => _issueRepo.GetByManager(managerId)
                .GroupBy(issue => issue.Status)
                .ToDictionary(
                    group => group.Key.ToString(),
                    group => group.AsEnumerable()
                );

        public IDictionary<string, IEnumerable<Issue>> GetByPriority(Guid managerId)
            => _issueRepo.GetByManager(managerId)
                .GroupBy(issue => issue.Priority)
                .ToDictionary(
                    group => group.Key.ToString(),
                    group => group.AsEnumerable()
                );

        public IEnumerable<Issue> GetIncompleteIssues(Guid managerId)
            => _issueRepo.GetByManager(managerId)
                .Where(i => i.CompletedAt is null);

        public IEnumerable<Issue> GetOverdueIssues(Guid managerId)
            => _issueRepo.GetByManager(managerId)
                .Where(i => i.IsOverdue());
    }
}
