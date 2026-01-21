using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Infrastructure.Interfaces
{
    public interface IIssueRepository : IRepository<Issue>
    {
        IEnumerable<Issue> GetByProject(Guid projectId);
        IEnumerable<Issue> GetByStatus(IssueStatus status);
        IEnumerable<Issue> GetByPriority(IssuePriority priority);
        IEnumerable<Issue> GetByAssignee(Guid developerId);
        IEnumerable<Issue> GetIncomplete();
    }
}
