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
            var user = _userService.GetById(userId);

            if (user.Role == UserRole.Manager)
            {
                var projects = _projectService.GetProjectsManagedBy(userId);
                foreach (var project in projects)
                {
                    _projectService.AssignManager(project.Id, newManagerId, currentUser);
                }

                var issues = _issueService.GetByManager(userId);
                foreach (var issue in issues)
                {
                    _issueService.AssignManager(issue.Id, newManagerId, currentUser);
                }
            }

            if (user.Role == UserRole.Developer)
            {
                var issues = _issueService.GetByDeveloper(userId);
                foreach (var issue in issues)
                {
                    _issueService.UnassignIssue(issue.Id, currentUser);
                }
            }

            _userService.DeleteUser(userId);
        }
    }
}
