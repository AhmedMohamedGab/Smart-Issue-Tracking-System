using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IProjectService
    {
        Project CreateProject(string name, string description, User currentUser);
        void RenameProject(Guid projectId, string newName, User currentUser);

        IEnumerable<Project> GetAllProjects();
        IEnumerable<Project> GetProjectsManagedBy(Guid managerId);
        Project GetById(Guid projectId);
    }
}
