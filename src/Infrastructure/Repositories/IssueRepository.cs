using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Infrastructure.Repositories
{
    public class IssueRepository : JsonRepository<Issue>, IIssueRepository
    {
        protected override string FilePath => "issues.json";

        public IEnumerable<Issue> GetByAssignee(Guid developerId)
            => _items.Where(i => i.AssigneeId == developerId);

        public IEnumerable<Issue> GetByPriority(IssuePriority priority)
            => _items.Where(i => i.Priority == priority);

        public IEnumerable<Issue> GetByProject(Guid projectId)
            => _items.Where(i => i.ProjectId == projectId);

        public IEnumerable<Issue> GetByStatus(IssueStatus status)
            => _items.Where(i => i.Status == status);

        public IEnumerable<Issue> GetIncomplete()
            => _items.Where(i => i.CompletedAt is not null);
    }
}
