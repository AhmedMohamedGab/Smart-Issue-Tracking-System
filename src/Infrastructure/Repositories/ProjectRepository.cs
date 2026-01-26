using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Infrastructure.Repositories
{
    public class ProjectRepository : JsonRepository<Project>, IProjectRepository
    {
        protected override string FilePath => "projects.json";

        public IEnumerable<Project> GetByManager(Guid managerId)
            => _items.Where(proj => proj.ManagerId == managerId);
    }
}
