using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    /// <summary>
    /// Provides authentication-related operations for the application,
    /// including user login, logout, and authentication state tracking.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates a user using their email address and establishes
        /// an authenticated session.
        /// </summary>
        /// <param name="email">
        /// The email address of the user attempting to log in.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the user does not exist or authentication fails.
        /// </exception>
        void Login(string email);

        /// <summary>
        /// Logs out the currently authenticated user and clears
        /// the authentication state.
        /// </summary>
        void Logout();

        /// <summary>
        /// Gets the currently authenticated user.
        /// </summary>
        /// <returns>
        /// The authenticated <see cref="User"/> instance.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no user is currently authenticated.
        /// </exception>
        User GetCurrentUser();

        /// <summary>
        /// Determines whether a user is currently authenticated.
        /// </summary>
        /// <returns>
        /// <c>true</c> if a user is authenticated; otherwise, <c>false</c>.
        /// </returns>
        bool IsAuthenticated();
    }
}
