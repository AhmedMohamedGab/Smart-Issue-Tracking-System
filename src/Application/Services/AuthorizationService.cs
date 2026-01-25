using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        //public void EnsureCanCreateUser(User user)
        //{
        //    if (user.Role != UserRole.Admin)
        //        throw new UnauthorizedAccessException("Only admins can create new users.");
        //}

        //public void EnsureCanCreateProject(User user)
        //{
        //    if (user.Role != UserRole.Admin && user.Role != UserRole.Manager)
        //        throw new UnauthorizedAccessException("Only admins and project managers can create new projects.");
        //}

        public void EnsureCanManageProject(Project project, User user)
        {
            if (user.Role != UserRole.Admin && (user.Role != UserRole.Manager || project.ManagerId != user.Id))
                throw new UnauthorizedAccessException("Only admins and the project manager can manage this project.");
        }

        //public void EnsureCanCreateIssue(User user)
        //{
        //    if (user.Role != UserRole.Manager)
        //        throw new UnauthorizedAccessException("Only project managers can create issues.");
        //}

        public void EnsureCanAssignIssue(Issue issue, User user)
        {
            if (user.Role != UserRole.Manager || issue.ManagerId != user.Id)
                throw new UnauthorizedAccessException("Only the project manager can assign this issue.");
        }

        public void EnsureCanChangeIssueStatus(Issue issue, User user)
        {
            if (user.Role != UserRole.Developer || issue.AssigneeId != user.Id)
                throw new UnauthorizedAccessException("Only the assigned developer can change the status of this issue.");
        }

        public void EnsureCanChangeIssueDuedate(Issue issue, User user)
        {
            if (user.Role != UserRole.Manager || issue.ManagerId != user.Id)
                throw new UnauthorizedAccessException("Only the project manager can change the due date of this issue.");
        }

        public void EnsureCanCloseIssue(Issue issue, User user)
        {
            if (user.Role != UserRole.Manager || issue.ManagerId != user.Id)
                throw new UnauthorizedAccessException("Only the project manager can close this issue.");
        }

        public void EnsureCanViewProjectIssues(Project project, User user)
        {
            if (user.Role != UserRole.Manager || project.ManagerId != user.Id)
                throw new UnauthorizedAccessException("Only the project manager can get issues of this project.");
        }
    }
}
