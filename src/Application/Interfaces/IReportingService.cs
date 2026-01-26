using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IReportingService
    {
        int GetOpenIssueCountForProject(Guid projectId, User currentUser);
        IDictionary<string, int> GetIssueCountByStatus(Guid projectId, User currentUser);
        int GetOverdueIssueCountForProject(Guid projectId, User currentUser);

        int GetDeveloperWorkload(Guid developerId);
        double GetProjectProgress(Guid projectId, User currentUser);
    }
}
