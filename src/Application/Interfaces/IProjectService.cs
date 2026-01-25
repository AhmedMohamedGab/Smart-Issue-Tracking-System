using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IProjectService
    {
        Project CreateProject(string name, string description, string ManagerEmail);
        void RenameProject(Guid projectId, string newName, User currentUser);

        IEnumerable<Project> GetAllProjects();
        Project? GetById(Guid projectId);
        IEnumerable<Project> GetProjectsManagedBy(Guid managerId);
    }
}
