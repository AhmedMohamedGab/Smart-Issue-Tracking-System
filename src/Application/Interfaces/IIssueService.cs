using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IIssueService
    {
        Issue CreateIssue(Guid projectId, string title, string description, User currentUser);
        void AssignIssue(Guid issueId, Guid developerId, User currentUser);
        void ChangeStatus(Guid issueId, IssueStatus newStatus, User currentUser);
        void CloseIssue(Guid issueId, User currentUser);

        Issue GetById(Guid issueId);
        IEnumerable<Issue> GetByProject(Guid projectId);
        IEnumerable<Issue> GetByAssignee(Guid developerId);
        IEnumerable<Issue> GetByStatus(IssueStatus status);
        IEnumerable<Issue> GetForUser(User user);
        IEnumerable<Issue> GetOverdueIssues();
    }
}
