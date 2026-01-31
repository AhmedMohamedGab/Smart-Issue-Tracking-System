using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class ProjectLifecycleService : IProjectLifecycleService
    {
        private readonly IProjectService _projectService;
        private readonly IIssueService _issueService;

        public ProjectLifecycleService(
            IProjectService projectService,
            IIssueService issueService)
        {
            _projectService = projectService;
            _issueService = issueService;
        }

        public void EndProject(Guid projectId, User currentUser)
        {
            _projectService.EndProject(projectId, currentUser);

            var issues = _issueService.GetByProject(projectId, currentUser);

            foreach (var issue in issues)
            {
                _issueService.CloseIssue(issue.Id, currentUser);
            }
        }

        public void ReassignProject(Guid projectId, Guid newManagerId, User currentUser)
        {
            var project = _projectService.GetById(projectId);

            _projectService.AssignManager(projectId, newManagerId, currentUser);

            var issues = _issueService
                .GetByProject(projectId, currentUser)
                .Where(issue => issue.ManagerId == project.ManagerId);

            foreach (var issue in issues)
            {
                _issueService.AssignManager(issue.Id, newManagerId, currentUser);
            }
        }
    }
}
