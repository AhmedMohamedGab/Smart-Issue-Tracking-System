using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public void EnsureCanManageProject(Project project, User user)
        {
            if (user.Role != UserRole.Admin && (user.Role != UserRole.Manager || project.ManagerId != user.Id))
                throw new UnauthorizedAccessException("Only admins and the project manager can manage this project.");
        }

        public void EnsureCanManageIssue(Issue issue, User user)
        {
            if (user.Role != UserRole.Admin && (user.Role != UserRole.Manager || issue.ManagerId != user.Id))
                throw new UnauthorizedAccessException("Only admins and the project manager can manage this issue.");
        }

        public void EnsureCanViewProjectIssues(Project project, User user)
        {
            if (user.Role != UserRole.Manager || project.ManagerId != user.Id)
                throw new UnauthorizedAccessException("Only the project manager can get issues of this project.");
        }

        public void EnsureCanChangeIssueStatus(Issue issue, User user)
        {
            if (user.Role != UserRole.Developer || issue.AssigneeId != user.Id)
                throw new UnauthorizedAccessException("Only the assigned developer can change the status of this issue.");
        }
    }
}
