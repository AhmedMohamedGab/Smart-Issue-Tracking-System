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
            Name = name;
            Email = email;
            Role = role;
        }

        internal void EditInfo(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public override string ToString()
        {
            return $"\n- User: {Name}\n" +
                $"\t- Email: {Email}\n" +
                $"\t- Role: {Role}\n" +
                $"\t- ID: {Id}";
        }
    }
}
