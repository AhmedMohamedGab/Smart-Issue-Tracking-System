using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    /// <summary>
    /// Provides operations related to project management, such as creating projects, renaming them,
    /// assigning managers, and ending projects. It also includes methods for retrieving project information.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Creates a new project with the specified name, description, and manager.
        /// </summary>
        /// <param name="name">
        /// The name of the project to be created.
        /// </param>
        /// <param name="description">
        /// A brief description of the project.
        /// </param>
        /// <param name="ManagerEmail">
        /// The email of the manager to be assigned to the project.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the specified manager does not exist.
        /// </exception>
        void CreateProject(string name, string description, string ManagerEmail);

        /// <summary>
        /// Renames an existing project with the specified new name.
        /// </summary>
        /// <param name="projectId">
        /// The unique identifier of the project to be renamed.
        /// </param>
        /// <param name="newName">
        /// The new name for the project.
        /// </param>
        /// <param name="currentUser">
        /// The user attempting to rename the project, used for authorization checks.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the project does not exist.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the current user does not have permission to rename the project.
        /// </exception>
        void RenameProject(Guid projectId, string newName, User currentUser);

        /// <summary>
        /// Assigns a new manager to an existing project.
        /// </summary>
        /// <param name="projectId">
        /// The unique identifier of the project for which the manager is to be assigned.
        /// </param>
        /// <param name="newManagerId">
        /// The unique identifier of the new manager to be assigned to the project.
        /// </param>
        /// <param name="currentUser">
        /// The user attempting to assign the new manager, used for authorization checks.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the project does not exist, the project has already ended, or the new manager does not exist.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the current user does not have permission to assign a new manager to the project.
        /// </exception>
        void AssignManager(Guid projectId, Guid newManagerId, User currentUser);

        /// <summary>
        /// Ends an existing project, marking it as completed and preventing further modifications.
        /// </summary>
        /// <param name="projectId">
        /// The unique identifier of the project to be ended.
        /// </param>
        /// <param name="currentUser">
        /// The user attempting to end the project, used for authorization checks.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the project does not exist or the project has already ended.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the current user does not have permission to end the project.
        /// </exception>
        void EndProject(Guid projectId, User currentUser);

        /// <summary>
        /// Retrieves all projects in the system, including their details and manager information.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="Project"/> objects representing all projects in the system.
        /// </returns>
        IEnumerable<Project> GetAllProjects();

        /// <summary>
        /// Retrieves a specific project by its unique identifier, including its details and information.
        /// </summary>
        /// <param name="projectId">
        /// The unique identifier of the project to be retrieved.
        /// </param>
        /// <returns>
        /// A <see cref="Project"/> object representing the project with the specified identifier,
        /// including its details and information.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the project with the specified identifier does not exist.
        /// </exception>
        Project GetById(Guid projectId);

        /// <summary>
        /// Retrieves all projects that are managed by a specific manager, identified by their unique identifier.
        /// </summary>
        /// <param name="managerId">
        /// The unique identifier of the manager whose projects are to be retrieved.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Project"/> objects representing all projects
        /// that are managed by the specified manager, including their details and information.
        /// </returns>
        IEnumerable<Project> GetProjectsManagedBy(Guid managerId);
    }
}
