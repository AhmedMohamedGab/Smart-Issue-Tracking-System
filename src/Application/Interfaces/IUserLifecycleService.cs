using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    /// <summary>
    /// Provides operations for managing the lifecycle of user accounts, including deletion and reassignment of
    /// responsibilities.
    /// </summary>
    public interface IUserLifecycleService
    {
        /// <summary>
        /// Deletes a user from the system and reassigns their responsibilities to another user if manager,
        /// or unassigns them if developer.
        /// </summary>
        /// <param name="userId">
        /// The ID of the user to be deleted.
        /// </param>
        /// <param name="newManagerId">
        /// The ID of the new manager to whom the responsibilities will be reassigned. This is required if the user is
        /// a manager. If the user is a developer, this parameter is ignored and responsibilities will be unassigned.
        /// </param>
        /// <param name="currentUser">
        /// The user performing the deletion operation, used for authorization purposes.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the user to be deleted does not exist, or when the specified new manager does not exist
        /// or is not a manager (if required).
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the current user does not have sufficient permissions to delete the specified user
        /// or reassign responsibilities.
        /// </exception>
        void DeleteUser(Guid userId, Guid newManagerId, User currentUser);
    }
}
