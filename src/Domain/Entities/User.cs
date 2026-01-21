using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Domain.Entities
{
    public abstract class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserRole Role { get; protected set; }

        protected User(string name, string email)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
    }
}
