using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Infrastructure.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> GetByManager(Guid managerId);
    }
}
