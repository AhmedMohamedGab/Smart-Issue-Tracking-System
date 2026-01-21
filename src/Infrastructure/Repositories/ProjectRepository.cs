using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Infrastructure.Repositories
{
    public class ProjectRepository : JsonRepository<Project>, IProjectRepository
    {
        protected override string FilePath => "projects.json";

        public IEnumerable<Project> GetByCreator(Guid creatorId)
            => _items.Where(p => p.CreatorId == creatorId);
    }
}
