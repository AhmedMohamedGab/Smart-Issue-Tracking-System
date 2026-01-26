using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Infrastructure.Repositories
{
    public class IssueRepository : JsonRepository<Issue>, IIssueRepository
    {
        protected override string FilePath => "issues.json";

        public IEnumerable<Issue> GetByProject(Guid projectId)
            => _items.Where(issue => issue.ProjectId == projectId);

        public IEnumerable<Issue> GetByManager(Guid managerId)
            => _items.Where(issue => issue.ManagerId == managerId);

        public IEnumerable<Issue> GetByDeveloper(Guid developerId)
            => _items.Where(issue => issue.AssigneeId == developerId);
    }
}
