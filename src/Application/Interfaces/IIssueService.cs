using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IIssueService
    {
        Issue CreateIssue(
            string title,
            string description,
            IssuePriority priority,
            DateTime dueDate,
            Guid managerId,
            Guid projectId);
        void AssignManager(Guid issueId, Guid newManagerId, User currentUser);
        void AssignIssue(Guid issueId, string developerEmail, User currentUser);
        void UnassignIssue(Guid issueId, User currentUser);
        void ChangeStatus(Guid issueId, IssueStatus newStatus, User currentUser);
        void ChangeDuedate(Guid issueId, DateTime newDuedate, User currentUser);
        void CloseIssue(Guid issueId, User currentUser);
        void DeleteIssue(Guid issueId, User currentUser);

        Issue GetById(Guid issueId);
        IEnumerable<Issue> GetByProject(Guid projectId, User currentUser);
        IEnumerable<Issue> GetByManager(Guid managerId);
        IEnumerable<Issue> GetByDeveloper(Guid developerId);
        IDictionary<string, IEnumerable<Issue>> GetForManager(Guid managerId);
        IDictionary<string, IEnumerable<Issue>> GetByAssignee(Guid managerId);
        IDictionary<string, IEnumerable<Issue>> GetForDeveloper(Guid developerId);
        IDictionary<string, IEnumerable<Issue>> GetByStatus(Guid managerId);
        IDictionary<string, IEnumerable<Issue>> GetByPriority(Guid managerId);
        IEnumerable<Issue> GetIncompleteIssues(Guid managerId);
        IEnumerable<Issue> GetOverdueIssues(Guid managerId);
    }
}
