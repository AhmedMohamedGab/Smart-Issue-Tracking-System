using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepo;
        private readonly IUserRepository _userRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IAuthorizationService _auth;

        public IssueService(
            IIssueRepository issueRepository,
            IUserRepository userRepository,
            IProjectRepository projectRepository,
            IAuthorizationService authorizationService)
        {
            _issueRepo = issueRepository;
            _userRepo = userRepository;
            _projectRepo = projectRepository;
            _auth = authorizationService;
        }

        public Issue CreateIssue(
            string title,
            string description,
            IssuePriority priority,
            DateTime dueDate,
            Guid managerId,
            Guid projectId)
        {
            var newIssue = new Issue(title, description, priority, dueDate, managerId, projectId);
            _issueRepo.Add(newIssue);

            return newIssue;
        }

        public void AssignIssue(Guid issueId, string developerEmail, User currentUser)
        {
            var issue = _issueRepo.GetById(issueId) ?? throw new InvalidOperationException("Issue not found");
            var developer = _userRepo.GetByEmail(developerEmail) ?? throw new InvalidOperationException("Developer not found");

            _auth.EnsureCanAssignIssue(issue, currentUser);

            issue.AssignTo(developer.Id);
            _issueRepo.Update(issue);
        }

        public void ChangeStatus(Guid issueId, IssueStatus newStatus, User currentUser)
        {
            var issue = _issueRepo.GetById(issueId) ?? throw new InvalidOperationException("Issue not found");

            _auth.EnsureCanChangeIssueStatus(issue, currentUser);

            issue.ChangeStatus(newStatus);
            _issueRepo.Update(issue);
        }

        public void ChangeDuedate(Guid issueId, DateTime newDuedate, User currentUser)
        {
            var issue = _issueRepo.GetById(issueId) ?? throw new InvalidOperationException("Issue not found");

            _auth.EnsureCanChangeIssueDuedate(issue, currentUser);

            issue.ChangeDueDate(newDuedate);
            _issueRepo.Update(issue);
        }

        public void CloseIssue(Guid issueId, User currentUser)
        {
            var issue = _issueRepo.GetById(issueId) ?? throw new InvalidOperationException("Issue not found");

            _auth.EnsureCanCloseIssue(issue, currentUser);

            issue.ChangeStatus(IssueStatus.Closed);
            _issueRepo.Update(issue);
        }

        public Issue? GetById(Guid issueId)
            => _issueRepo.GetById(issueId) ?? throw new InvalidOperationException("Issue not found");

        public IEnumerable<Issue> GetByProject(Guid projectId, User currentUser)
        {
            var project = _projectRepo.GetById(projectId) ?? throw new InvalidOperationException("Project not found");

            _auth.EnsureCanViewProjectIssues(project, currentUser);

            return _issueRepo.GetByProject(projectId);
        }

        public IDictionary<string, IEnumerable<Issue>> GetForManager(Guid managerId)
        {
            var managerIssues = _issueRepo.GetByManager(managerId);
            var managerProjects = _projectRepo.GetByManager(managerId).Select(p => new { Id = p.Id, Name = p.Name });

            var managerIssuesGroupedByProjectNames = managerProjects.ToDictionary(
                proj => proj.Name,
                proj => managerIssues.Where(issue => issue.ProjectId == proj.Id)
            );

            return managerIssuesGroupedByProjectNames;
        }

        public IDictionary<string, IEnumerable<Issue>> GetByAssignee(Guid managerId)
        {
            var managerIssues = _issueRepo.GetByManager(managerId);
            var developers = _userRepo.GetAll().Where(u => u.Role == UserRole.Developer);

            var managerIssuesGroupedByDeveloperEmails = developers.GroupJoin(
                managerIssues,
                dev => dev.Id,
                issue => issue.AssigneeId,
                (dev, issues) => new { DeveloperEmail = dev.Email, Issues = issues }
            ).ToDictionary(
                g => g.DeveloperEmail,
                g => g.Issues
            );

            return managerIssuesGroupedByDeveloperEmails;
        }

        public IDictionary<string, IEnumerable<Issue>> GetForDeveloper(Guid developerId)
        {
            var developerIssues = _issueRepo.GetByDeveloper(developerId);
            var projects = _projectRepo.GetAll();

            var developerIssuesGroupedByProjectNames = projects.GroupJoin(
                developerIssues,
                proj => proj.Id,
                issue => issue.ProjectId,
                (proj, issues) => new { ProjectName = proj.Name, Issues = issues }
            ).ToDictionary(
                g => g.ProjectName,
                g => g.Issues
            );

            return developerIssuesGroupedByProjectNames;
        }

        public IDictionary<string, IEnumerable<Issue>> GetByStatus(Guid managerId)
        {
            var managerIssues = _issueRepo.GetByManager(managerId);

            var managerIssuesGroupedByStatus = managerIssues
                .GroupBy(issue => issue.Status)
                .ToDictionary(
                    g => g.Key.ToString(),
                    g => g.AsEnumerable()
                );

            return managerIssuesGroupedByStatus;
        }

        public IDictionary<string, IEnumerable<Issue>> GetByPriority(Guid managerId)
        {
            var managerIssues = _issueRepo.GetByManager(managerId);

            var managerIssuesGroupedByPriority = managerIssues
                .GroupBy(issue => issue.Priority)
                .ToDictionary(
                    g => g.Key.ToString(),
                    g => g.AsEnumerable()
                );

            return managerIssuesGroupedByPriority;
        }

        public IEnumerable<Issue> GetIncompleteIssues(Guid managerId)
        {
            var managerIssues = _issueRepo.GetByManager(managerId);

            var incompleteIssues = managerIssues.Where(i => i.CompletedAt is null);

            return incompleteIssues;

        }

        public IEnumerable<Issue> GetOverdueIssues(Guid managerId)
        {
            var managerIssues = _issueRepo.GetByManager(managerId);

            var overdueIssues = managerIssues.Where(i => i.DueDate < DateTime.UtcNow && i.CompletedAt is null);

            return overdueIssues;
        }
    }
}
