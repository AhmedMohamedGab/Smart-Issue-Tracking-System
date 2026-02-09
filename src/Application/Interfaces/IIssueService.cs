using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for the IssueService,
    /// which provides methods for managing issues within the Smart Issue Tracking System.
    /// </summary>
    public interface IIssueService
    {
        /// <summary>
        /// Creates a new issue with the specified details and associates it with a project and manager.
        /// </summary>
        /// <param name="title">
        /// The title of the issue.
        /// </param>
        /// <param name="description">
        /// The description of the issue.
        /// </param>
        /// <param name="priority">
        /// The priority level of the issue, represented as an integer corresponding to the IssuePriority enum.
        /// </param>
        /// <param name="dueDate">
        /// The due date for resolving the issue.
        /// </param>
        /// <param name="managerId">
        /// The unique identifier of the manager responsible for overseeing the issue.
        /// </param>
        /// <param name="projectId">
        /// The unique identifier of the project to which the issue belongs.
        /// </param>
        void CreateIssue(
            string title,
            string description,
            int priority,
            DateTime dueDate,
            Guid managerId,
            Guid projectId);

        /// <summary>
        /// Assigns a new manager to the specified issue,
        /// allowing the new manager to oversee and manage the issue's progress.
        /// </summary>
        /// <param name="issueId">
        /// The unique identifier of the issue for which the manager assignment is being made.
        /// </param>
        /// <param name="newManagerId">
        /// The unique identifier of the new manager to be assigned to the issue.
        /// </param>
        /// <param name="currentUser">
        /// The user performing the manager assignment operation, used for authentication and authorization purposes.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the issue does not exist, the issue is closed, or the new manager does not exist.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user performing the operation is not authorized.
        /// </exception>
        void AssignManager(Guid issueId, Guid newManagerId, User currentUser);

        /// <summary>
        /// Assigns a developer to the specified issue, allowing the developer to change the issue's status.
        /// </summary>
        /// <param name="issueId">
        /// The unique identifier of the issue for which the developer assignment is being made.
        /// </param>
        /// <param name="developerEmail">
        /// The email address of the developer to be assigned to the issue, used to identify the developer in the system.
        /// </param>
        /// <param name="currentUser">
        /// The user performing the developer assignment operation, used for authentication and authorization purposes.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the issue does not exist, the issue is closed, or the developer does not exist.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user performing the operation is not authorized.
        /// </exception>
        void AssignIssue(Guid issueId, string developerEmail, User currentUser);

        /// <summary>
        /// Unassigns the developer from the specified issue,
        /// removing the developer's association with the issue and preventing them from changing the issue's status.
        /// </summary>
        /// <param name="issueId">
        /// The unique identifier of the issue from which the developer is being unassigned.
        /// </param>
        /// <param name="currentUser">
        /// The user performing the developer unassignment operation, used for authentication and authorization purposes.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the issue does not exist, or the issue is closed.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user performing the operation is not authorized.
        /// </exception>
        void UnassignIssue(Guid issueId, User currentUser);

        /// <summary>
        /// Changes the status of the specified issue to a new status,
        /// allowing for tracking the issue's progress and workflow.
        /// </summary>
        /// <param name="issueId">
        /// The unique identifier of the issue for which the status change is being made.
        /// </param>
        /// <param name="newStatus">
        /// The new status to be assigned to the issue, represented as an integer corresponding to the IssueStatus enum.
        /// </param>
        /// <param name="currentUser">
        /// The user performing the status change operation, used for authentication and authorization purposes.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the issue does not exist, or the issue is closed.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user performing the operation is not authorized.
        /// </exception>
        void ChangeStatus(Guid issueId, int newStatus, User currentUser);

        /// <summary>
        /// Changes the due date of the specified issue to a new due date.
        /// </summary>
        /// <param name="issueId">
        /// The unique identifier of the issue for which the due date change is being made.
        /// </param>
        /// <param name="newDuedate">
        /// The new due date to be assigned to the issue.
        /// </param>
        /// <param name="currentUser">
        /// The user performing the due date change operation, used for authentication and authorization purposes.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the issue does not exist, or the issue is closed or done, or the new due date is in the past.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user performing the operation is not authorized.
        /// </exception>
        void ChangeDuedate(Guid issueId, DateTime newDuedate, User currentUser);

        /// <summary>
        /// Closes the specified issue by changing its status to "Closed",
        /// indicating that the issue has been resolved and is no longer active or requires attention.
        /// </summary>
        /// <param name="issueId">
        /// The unique identifier of the issue to be closed.
        /// </param>
        /// <param name="currentUser">
        /// The user performing the issue closure operation, used for authentication and authorization purposes.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the issue does not exist, or the issue is closed.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user performing the operation is not authorized.
        /// </exception>
        void CloseIssue(Guid issueId, User currentUser);

        /// <summary>
        /// Deletes the specified issue from the system, removing all associated data and records related to the issue.
        /// </summary>
        /// <param name="issueId">
        /// The unique identifier of the issue to be deleted.
        /// </param>
        /// <param name="currentUser">
        /// The user performing the issue deletion operation, used for authentication and authorization purposes.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the issue does not exist.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user performing the operation is not authorized.
        /// </exception>
        void DeleteIssue(Guid issueId, User currentUser);

        /// <summary>
        /// Retrieves the issue with the specified unique identifier,
        /// allowing for viewing and managing the issue's details and information.
        /// </summary>
        /// <param name="issueId">
        /// The unique identifier of the issue to be retrieved.
        /// </param>
        /// <returns>
        /// The issue with the specified unique identifier, including its details and information.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the issue does not exist.
        /// </exception>
        Issue GetById(Guid issueId);

        /// <summary>
        /// Retrieves all issues associated with the specified project,
        /// allowing for viewing and managing the issues within the context of the project.
        /// </summary>
        /// <param name="projectId">
        /// The unique identifier of the project for which the issues are being retrieved.
        /// </param>
        /// <param name="currentUser">
        /// The user performing the issue retrieval operation, used for authentication and authorization purposes.
        /// </param>
        /// <returns>
        /// A collection of issues associated with the specified project, including their details and information.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the project does not exist.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Thrown when the user performing the operation is not authorized.
        /// </exception>
        IEnumerable<Issue> GetByProject(Guid projectId, User currentUser);

        /// <summary>
        /// Retrieves all issues associated with the specified manager,
        /// allowing for viewing and managing the issues that the manager is responsible for overseeing and managing.
        /// </summary>
        /// <param name="managerId">
        /// The unique identifier of the manager for which the issues are being retrieved.
        /// </param>
        /// <returns>
        /// A collection of issues associated with the specified manager, including their details and information.
        /// </returns>
        IEnumerable<Issue> GetByManager(Guid managerId);

        /// <summary>
        /// Retrieves all issues assigned to the specified developer,
        /// allowing for viewing and managing the issues that the developer is responsible for.
        /// </summary>
        /// <param name="developerId">
        /// The unique identifier of the developer for which the issues are being retrieved.
        /// </param>
        /// <returns>
        /// A collection of issues associated with the specified developer, including their details and information.
        /// </returns>
        IEnumerable<Issue> GetByDeveloper(Guid developerId);

        /// <summary>
        /// Retrieves all issues associated with the specified manager, grouped by their associated project names,
        /// allowing for viewing and managing the issues within the context of their respective projects,
        /// and the manager's responsibilities.
        /// </summary>
        /// <param name="managerId">
        /// The unique identifier of the manager for which the issues are being retrieved.
        /// </param>
        /// <returns>
        /// A dictionary where the key is the project name and the value is a collection of issues associated with
        /// the specified manager, including their details and information, grouped by their respective project names.
        /// </returns>
        IDictionary<string, IEnumerable<Issue>> GetForManager(Guid managerId);

        /// <summary>
        /// Retrieves all issues associated with the specified manager, grouped by their associated assignee's email,
        /// allowing for viewing and managing the issues within the context of their respective assignees,
        /// and the manager's responsibilities.
        /// </summary>
        /// <param name="managerId">
        /// The unique identifier of the manager for which the issues are being retrieved.
        /// </param>
        /// <returns>
        /// A dictionary where the key is the assignee's email and the value is a collection of issues associated with
        /// the specified manager, including their details and information, grouped by their respective assignees' emails.
        /// </returns>
        IDictionary<string, IEnumerable<Issue>> GetByAssignee(Guid managerId);

        /// <summary>
        /// Retrieves all issues associated with the specified developer, grouped by their associated project names,
        /// allowing for viewing and managing the issues within the context of their respective projects,
        /// and the developer's responsibilities.
        /// </summary>
        /// <param name="developerId">
        /// The unique identifier of the developer for which the issues are being retrieved.
        /// </param>
        /// <returns>
        /// A dictionary where the key is the project name and the value is a collection of issues associated with
        /// the specified developer, including their details and information, grouped by their respective project names.
        /// </returns>
        IDictionary<string, IEnumerable<Issue>> GetForDeveloper(Guid developerId);

        /// <summary>
        /// Retrieves all issues associated with the specified manager, grouped by their associated statuses,
        /// allowing for viewing and managing the issues within the context of their respective statuses,
        /// and the manager's responsibilities.
        /// </summary>
        /// <param name="managerId">
        /// The unique identifier of the manager for which the issues are being retrieved.
        /// </param>
        /// <returns>
        /// A dictionary where the key is the status and the value is a collection of issues associated with
        /// the specified manager, including their details and information, grouped by their respective statuses.
        /// </returns>
        IDictionary<string, IEnumerable<Issue>> GetByStatus(Guid managerId);

        /// <summary>
        /// Retrieves all issues associated with the specified manager, grouped by their associated priorities,
        /// allowing for viewing and managing the issues within the context of their respective priorities,
        /// and the manager's responsibilities.
        /// </summary>
        /// <param name="managerId">
        /// The unique identifier of the manager for which the issues are being retrieved.
        /// </param>
        /// <returns>
        /// A dictionary where the key is the priority and the value is a collection of issues associated with
        /// the specified manager, including their details and information, grouped by their respective priorities.
        /// </returns>
        IDictionary<string, IEnumerable<Issue>> GetByPriority(Guid managerId);

        /// <summary>
        /// Retrieves all incomplete issues associated with the specified manager,
        /// allowing for viewing and managing the issues that are still open and require attention
        /// within the context of the manager's responsibilities.
        /// </summary>
        /// <param name="managerId">
        /// The unique identifier of the manager for which the issues are being retrieved.
        /// </param>
        /// <returns>
        /// A collection of issues associated with the specified manager that are still open and require attention,
        /// including their details and information.
        /// </returns>
        IEnumerable<Issue> GetIncompleteIssues(Guid managerId);

        /// <summary>
        /// Retrieves all overdue issues associated with the specified manager,
        /// allowing for viewing and managing the issues that are past their due date and require immediate attention
        /// within the context of the manager's responsibilities.
        /// </summary>
        /// <param name="managerId">
        /// The unique identifier of the manager for which the issues are being retrieved.
        /// </param>
        /// <returns>
        /// A collection of issues associated with the specified manager that are past their due date
        /// and require immediate attention, including their details and information.
        /// </returns>
        IEnumerable<Issue> GetOverdueIssues(Guid managerId);
    }
}
