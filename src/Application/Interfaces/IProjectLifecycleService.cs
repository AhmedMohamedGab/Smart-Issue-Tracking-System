using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    /// <summary>
    /// An application service interface for handling project lifecycle operations
    /// such as ending a project and reassigning a project to a new manager.
    /// </summary>
    public interface IProjectLifecycleService
    {
        /// <summary>
        /// Ends a project by marking it as completed and closing all related issues.
        /// </summary>
        /// <param name="projectId">
        /// The unique identifier of the project to be ended.
        /// </param>
        /// <param name="currentUser">
        /// The user performing the operation, used for authentication and authorization checks.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the project does not exist, or when the project is already ended.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user does not have the necessary permissions to end the project.
        /// </exception>
        void EndProject(Guid projectId, User currentUser);

        /// <summary>
        /// Reassigns a project to a new manager by updating the project's manager information
        /// and reassigning all related issues to the new manager.
        /// </summary>
        /// <param name="projectId">
        /// The unique identifier of the project to be reassigned.
        /// </param>
        /// <param name="newManagerId">
        /// The unique identifier of the new manager to whom the project will be reassigned.
        /// </param>
        /// <param name="currentUser">
        /// The user performing the operation, used for authentication and authorization checks.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the project does not exist, the new manager does not exist, or when the project is already ended.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user does not have the necessary permissions to reassign the project.
        /// </exception>
        void ReassignProject(Guid projectId, Guid newManagerId, User currentUser);
    }
}
