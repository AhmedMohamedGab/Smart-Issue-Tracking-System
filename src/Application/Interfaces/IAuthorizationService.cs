using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IAuthorizationService
    {
        //void EnsureCanCreateUser(User user);

        //void EnsureCanCreateProject(User user);
        void EnsureCanManageProject(Project project, User user);

        //void EnsureCanCreateIssue(User user);
        void EnsureCanAssignIssue(Issue issue, User user);
        void EnsureCanChangeIssueStatus(Issue issue, User user);
        void EnsureCanChangeIssueDuedate(Issue issue, User user);
        void EnsureCanCloseIssue(Issue issue, User user);
        void EnsureCanViewProjectIssues(Project project, User user);
    }
}
