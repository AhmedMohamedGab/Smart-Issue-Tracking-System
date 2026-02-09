using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    /// <summary>
    /// Defines methods for generating reports and retrieving statistical information about issues and project progress
    /// within the system.
    /// </summary>
    public interface IReportingService
    {
        /// <summary>
        /// Gets the count of open issues for a specific project.
        /// </summary>
        /// <param name="projectId">
        /// The unique identifier of the project for which to retrieve the open issue count.
        /// </param>
        /// <param name="currentUser">
        /// The user requesting the information, used for authorization checks.
        /// </param>
        /// <returns>
        /// An integer representing the number of open issues associated with the specified project.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the specified project does not exist.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the current user does not have permission to view the project or its issues.
        /// </exception>
        int GetOpenIssueCountForProject(Guid projectId, User currentUser);

        /// <summary>
        /// Gets the count of issues grouped by their status for a specific project.
        /// </summary>
        /// <param name="projectId">
        /// The unique identifier of the project for which to retrieve the issue counts by status.
        /// </param>
        /// <param name="currentUser">
        /// The user requesting the information, used for authorization checks.
        /// </param>
        /// <returns>
        /// A dictionary where the key is issue status and the value is the count of issues in each status
        /// for the specified project.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the specified project does not exist.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the current user does not have permission to view the project or its issues.
        /// </exception>
        IDictionary<string, int> GetIssueCountByStatus(Guid projectId, User currentUser);

        /// <summary>
        /// Gets the count of overdue issues for a specific project.
        /// </summary>
        /// <param name="projectId">
        /// The unique identifier of the project for which to retrieve the overdue issue count.
        /// </param>
        /// <param name="currentUser">
        /// The user requesting the information, used for authorization checks.
        /// </param>
        /// <returns>
        /// An integer representing the number of overdue issues associated with the specified project.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the specified project does not exist.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the current user does not have permission to view the project or its issues.
        /// </exception>
        int GetOverdueIssueCountForProject(Guid projectId, User currentUser);

        /// <summary>
        /// Gets the workload of a developer, calculated as the total number of incomplete issues assigned to them
        /// across all projects.
        /// </summary>
        /// <param name="developerId">
        /// The unique identifier of the developer for whom to retrieve the workload information.
        /// </param>
        /// <returns>
        /// An integer representing the total number of incomplete issues assigned to the specified developer
        /// across all projects.
        /// </returns>
        int GetDeveloperWorkload(Guid developerId);

        /// <summary>
        /// Gets the overall progress of a project based on the status of its issues,
        /// calculated as a percentage of completed issues over total issues.
        /// </summary>
        /// <param name="projectId">
        /// The unique identifier of the project for which to retrieve the progress information.
        /// </param>
        /// <param name="currentUser">
        /// The user requesting the information, used for authorization checks.
        /// </param>
        /// <returns>
        /// A double representing the percentage of project completion, where 0% means no issues are completed
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the specified project does not exist.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the current user does not have permission to view the project or its issues.
        /// </exception>
        double GetProjectProgress(Guid projectId, User currentUser);
    }
}
