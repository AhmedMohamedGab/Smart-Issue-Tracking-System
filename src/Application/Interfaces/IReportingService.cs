using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IReportingService
    {
        int GetOpenIssueCountForProject(Guid projectId);
        int GetOverdueIssueCountForProject(Guid projectId);
        IDictionary<IssueStatus, int> GetIssueCountByStatus(Guid projectId);

        int GetDeveloperWorkload(Guid developerId);
        double GetProjectProgress(Guid projectId);
    }
}
