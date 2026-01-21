using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Domain.Entities
{
    public class Manager : ProjectCreatingUser
    {
        public Manager(string name, string email) : base(name, email)
        {
            Role = UserRole.Manager;
        }
    }
}
