using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IAuthorizationService
    {
        void EnsureCanManageProject(Project project, User user);

        void EnsureCanManageIssue(Issue issue, User user);
        void EnsureCanViewProjectIssues(Project project, User user);
        void EnsureCanChangeIssueStatus(Issue issue, User user);
    }
}
