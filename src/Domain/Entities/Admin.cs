using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Domain.Entities
{
    public class Admin : ProjectCreatingUser
    {
        public Admin(string name, string email) : base(name, email)
        {
            Role = UserRole.Admin;
        }
    }
}
