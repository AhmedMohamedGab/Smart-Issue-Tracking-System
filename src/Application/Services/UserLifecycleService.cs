using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class UserLifecycleService : IUserLifecycleService
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly IIssueService _issueService;

        public UserLifecycleService(
            IUserService userService,
            IProjectService projectService,
            IIssueService issueService)
        {
            _userService = userService;
            _projectService = projectService;
            _issueService = issueService;
        }

        public void DeleteUser(Guid userId, Guid newManagerId, User currentUser)
        {
            var user = _userService.GetById(userId);    // Ensure user exists

            if (user.Role == UserRole.Manager)
            {
                // Ensure new manager exists and is a manager
                var newManager = _userService.GetById(newManagerId);
                if (newManager.Role != UserRole.Manager)
                    throw new InvalidOperationException("New manager must have Manager role.");

                // Reassign active projects of the deleted manager to the new manager
                var projects = _projectService.GetProjectsManagedBy(userId);
                foreach (var project in projects)
                    if (project.EndDate is null)
                        _projectService.AssignManager(project.Id, newManagerId, currentUser);

                // Reassign open issues of the deleted manager to the new manager
                var issues = _issueService.GetByManager(userId);
                foreach (var issue in issues)
                    if (issue.Status != IssueStatus.Closed)
                        _issueService.AssignManager(issue.Id, newManagerId, currentUser);
            }

            if (user.Role == UserRole.Developer)
            {
                // Unassign open issues of the deleted developer
                var issues = _issueService.GetByDeveloper(userId);
                foreach (var issue in issues)
                    if (issue.Status != IssueStatus.Closed)
                        _issueService.UnassignIssue(issue.Id, currentUser);
            }

            _userService.DeleteUser(userId);
        }
    }
}
