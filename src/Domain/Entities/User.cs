using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserRole Role { get; private set; }

        public User(string name, string email, UserRole role)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Role = role;
        }

        public void EditInfo(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
