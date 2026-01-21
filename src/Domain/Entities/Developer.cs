using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Domain.Entities
{
    public class Developer : User
    {
        public Developer(string name, string email) : base(name, email)
        {
            Role = UserRole.Developer;
        }
    }
}
