using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IIssueService _issueService;
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;

        public ReportingService(
            IIssueService issueService,
            IUserService userService,
            IProjectService projectService)
        {
            _issueService = issueService;
            _userService = userService;
            _projectService = projectService;
        }

        public int GetOpenIssueCountForProject(Guid projectId, User currentUser)
            => _issueService
                .GetByProject(projectId, currentUser)
                .Count(issue => issue.Status == IssueStatus.Open);

        public IDictionary<string, int> GetIssueCountByStatus(Guid projectId, User currentUser)
            => _issueService
                .GetByProject(projectId, currentUser)
                .GroupBy(issue => issue.Status)
                .ToDictionary(
                    group => group.Key.ToString(),
                    group => group.Count()
                );

        public int GetOverdueIssueCountForProject(Guid projectId, User currentUser)
            => _issueService
                .GetByProject(projectId, currentUser)
                .Count(issue => issue.IsOverdue());

        public int GetDeveloperWorkload(Guid developerId)
            => _issueService
                .GetByDeveloper(developerId)
                .Count(issue => issue.Status == IssueStatus.InProgress || issue.Status == IssueStatus.InReview);

        public double GetProjectProgress(Guid projectId, User currentUser)
        {
            var issues = _issueService.GetByProject(projectId, currentUser).ToList();

            if (!issues.Any())
                return 0;

            var doneCount = issues.Count(i => i.Status == IssueStatus.Done || i.Status == IssueStatus.Closed);

            return (double)doneCount / issues.Count * 100;
        }
    }
}
