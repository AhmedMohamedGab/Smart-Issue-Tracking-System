using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class IssueService : IIssueService
    {
        public void AssignIssue(Guid issueId, Guid developerId, User currentUser)
        {
            throw new NotImplementedException();
        }

        public void ChangeStatus(Guid issueId, IssueStatus newStatus, User currentUser)
        {
            throw new NotImplementedException();
        }

        public void CloseIssue(Guid issueId, User currentUser)
        {
            throw new NotImplementedException();
        }

        public Issue CreateIssue(Guid projectId, string title, string description, User currentUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Issue> GetByAssignee(Guid developerId)
        {
            throw new NotImplementedException();
        }

        public Issue GetById(Guid issueId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Issue> GetByProject(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Issue> GetByStatus(IssueStatus status)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Issue> GetForUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Issue> GetOverdueIssues()
        {
            throw new NotImplementedException();
        }
    }
}
