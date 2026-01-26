using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Infrastructure.Interfaces
{
    public interface IIssueRepository : IRepository<Issue>
    {
        IEnumerable<Issue> GetByProject(Guid projectId);
        IEnumerable<Issue> GetByManager(Guid managerId);
        IEnumerable<Issue> GetByDeveloper(Guid developerId);
    }
}
