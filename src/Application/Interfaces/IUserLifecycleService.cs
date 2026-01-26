using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IUserLifecycleService
    {
        void DeleteUser(Guid userId, Guid newManagerId, User currentUser);
    }
}
