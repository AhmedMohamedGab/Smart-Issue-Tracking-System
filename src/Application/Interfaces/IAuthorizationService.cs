using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    /// <summary>
    /// Provides authorization operations for the application,
    /// including managing projects and issues.
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Checks whether a user can manage a specific project.
        /// </summary>
        /// <param name="project">
        /// The project which the user is attempting to manage.
        /// </param>
        /// <param name="user">
        /// The user attempting to manage the project.
        /// </param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user is not an admin or the manager who manages the project.
        /// </exception>
        void EnsureCanManageProject(Project project, User user);

        /// <summary>
        /// Checks whether a user can manage a specific issue.
        /// </summary>
        /// <param name="issue">
        /// The issue which the user is attempting to manage.
        /// </param>
        /// <param name="user">
        /// The user attempting to manage the issue.
        /// </param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user is not an admin or the manager who manages the issue.
        /// </exception>
        void EnsureCanManageIssue(Issue issue, User user);

        /// <summary>
        /// Checks whether a user can view issues of a specific project.
        /// </summary>
        /// <param name="project">
        /// The project which the user is attempting to view its issues.
        /// </param>
        /// <param name="user">
        /// The user attempting to view issues of the project.
        /// </param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user is not the manager who manages the project.
        /// </exception>
        void EnsureCanViewProjectIssues(Project project, User user);

        /// <summary>
        /// Checks whether a user can change a specific issue status.
        /// </summary>
        /// <param name="issue">
        /// The issue which the user is attempting to change status.
        /// </param>
        /// <param name="user">
        /// The user attempting to change the status of the issue.
        /// </param>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user is not the assigned developer for this issue.
        /// </exception>
        void EnsureCanChangeIssueStatus(Issue issue, User user);
    }
}
