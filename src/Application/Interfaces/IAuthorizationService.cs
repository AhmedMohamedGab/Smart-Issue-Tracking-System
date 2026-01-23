using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IAuthorizationService
    {
        void EnsureCanCreateUser(User user);

        void EnsureCanCreateProject(User user);
        void EnsureCanManageProject(User user, Project project);

        void EnsureCanCreateIssue(User user);
        void EnsureCanCloseIssue(User user, Issue issue);
        void EnsureCanAssignIssue(User user, Issue issue);
        void EnsureCanChangeIssueStatus(User user, Issue issue);
    }
}
