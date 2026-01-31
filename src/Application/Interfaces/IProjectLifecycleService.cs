using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IProjectLifecycleService
    {
        void EndProject(Guid projectId, User currentUser);
        void ReassignProject(Guid projectId, Guid newManagerId, User currentUser);
    }
}
