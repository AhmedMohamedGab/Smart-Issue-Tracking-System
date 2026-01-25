using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Infrastructure.Repositories
{
    public class IssueRepository : JsonRepository<Issue>, IIssueRepository
    {
        protected override string FilePath => "issues.json";

        public IEnumerable<Issue> GetByProject(Guid projectId)
            => _items.Where(i => i.ProjectId == projectId);

        public IEnumerable<Issue> GetByManager(Guid managerId)
            => _items.Where(i => i.ManagerId == managerId);

        public IEnumerable<Issue> GetByDeveloper(Guid developerId)
            => _items.Where(i => i.AssigneeId == developerId);

        //public IEnumerable<Issue> GetByStatus(IssueStatus status)
        //    => _items.Where(i => i.Status == status);

        //public IEnumerable<Issue> GetByPriority(IssuePriority priority)
        //    => _items.Where(i => i.Priority == priority);

        //public IEnumerable<Issue> GetIncomplete()
        //    => _items.Where(i => i.CompletedAt is null);

        //public IEnumerable<Issue> GetOverdue()
        //    => _items.Where(i => i.DueDate < DateTime.UtcNow && i.CompletedAt is null);
    }
}
