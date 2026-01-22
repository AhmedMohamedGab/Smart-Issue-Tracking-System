using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public void EnsureCanAssignIssue(User user)
        {
            throw new NotImplementedException();
        }

        public void EnsureCanChangeIssueStatus(User user, Issue issue)
        {
            throw new NotImplementedException();
        }

        public void EnsureCanCreateIssue(User user)
        {
            throw new NotImplementedException();
        }

        public void EnsureCanCreateProject(User user)
        {
            throw new NotImplementedException();
        }

        public void EnsureCanCreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void EnsureCanManageProject(User user, Project project)
        {
            throw new NotImplementedException();
        }
    }
}
