using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Infrastructure.Repositories
{
    public class UserRepository : JsonRepository<User>, IUserRepository
    {
        protected override string FilePath => "users.json";

        public override void Add(User newUser)
        {
            var user = GetByEmail(newUser.Email);

            if (user is not null)
                throw new InvalidOperationException("A user with this email already exists.");

            base.Add(newUser);
        }

        public User? GetByEmail(string email)
            => _items.FirstOrDefault(user => user.Email == email);

        public IEnumerable<User> GetDevelopers()
            => _items.Where(user => user.Role == UserRole.Developer);
    }
}
