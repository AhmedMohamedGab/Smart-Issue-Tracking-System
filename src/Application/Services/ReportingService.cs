using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class ReportingService : IReportingService
    {
        public int GetDeveloperWorkload(Guid developerId)
        {
            throw new NotImplementedException();
        }

        public IDictionary<IssueStatus, int> GetIssueCountByStatus(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public int GetOpenIssueCountForProject(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public int GetOverdueIssueCountForProject(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public double GetProjectProgress(Guid projectId)
        {
            throw new NotImplementedException();
        }
    }
}
