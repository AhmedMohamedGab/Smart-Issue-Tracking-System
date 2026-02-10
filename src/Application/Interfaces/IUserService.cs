using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    /// <summary>
    /// Defines operations for managing user accounts, including creation, modification, deletion, and retrieval of user
    /// information.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates a new user account with the specified name, email, and role.
        /// </summary>
        /// <param name="name">
        /// The name of the user to be created.
        /// </param>
        /// <param name="email">
        /// The email address of the user to be created. Must be unique across all users.
        /// </param>
        /// <param name="role">
        /// The role of the user to be created, represented as an integer corresponding to the UserRole enum.
        /// Valid values are: 1 for Admin, 2 for Manager, and 3 for Developer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when a user with the same email already exists in the system.
        /// </exception>
        void CreateUser(string name, string email, int role);

        /// <summary>
        /// Edits the information of an existing user account, allowing changes to the name and email.
        /// </summary>
        /// <param name="name">
        /// The new name for the user. This can be the same as the current name if no change is desired.
        /// </param>
        /// <param name="email">
        /// The new email address for the user. This must be unique across all users and can be the same
        /// as the current email if no change is desired.
        /// </param>
        /// <param name="currentUser">
        /// The user whose information is to be edited.
        /// </param>
        void EditInfo(string name, string email, User currentUser);

        /// <summary>
        /// Deletes an existing user account identified by the specified user ID.
        /// </summary>
        /// <param name="userId">
        /// The unique identifier of the user to be deleted. The user must exist in the system for the deletion to succeed.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the user with the specified ID does not exist in the system.
        /// </exception>
        void DeleteUser(Guid userId);

        /// <summary>
        /// Retrieves a list of all user accounts in the system.
        /// </summary>
        /// <returns>
        /// A collection of User objects representing all users in the system. If no users exist,
        /// an empty collection is returned.
        /// </returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Retrieves a user account by its unique identifier.
        /// </summary>
        /// <param name="userId">
        /// The unique identifier of the user to be retrieved.
        /// </param>
        /// <returns>
        /// A User object representing the user with the specified ID.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the user with the specified ID does not exist in the system.
        /// </exception>
        User GetById(Guid userId);

        /// <summary>
        /// Retrieves a user account by its email address.
        /// </summary>
        /// <param name="email">
        /// The email of the user to be retrieved.
        /// </param>
        /// <returns>
        /// A User object representing the user with the specified email.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the user with the specified email does not exist in the system.
        /// </exception>
        User GetByEmail(string email);

        /// <summary>
        /// Retrieves all user accounts in the system that have the role of Developer.
        /// </summary>
        /// <returns>
        /// A collection of User objects representing all users with the Developer role. If no developers exist,
        /// an empty collection is returned.
        /// </returns>
        IEnumerable<User> GetDevelopers();
    }
}
