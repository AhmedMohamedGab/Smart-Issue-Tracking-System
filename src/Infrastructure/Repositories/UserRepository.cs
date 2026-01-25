using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Infrastructure.Repositories
{
    public class UserRepository : JsonRepository<User>, IUserRepository
    {
        protected override string FilePath => "users.json";

        public override void Add(User user)
        {
            var newUser = _items.FirstOrDefault(u => u.Email == user.Email);
            if (newUser is null)
            {
                base.Add(user);
            }
        }

        public User? GetByEmail(string email)
            => _items.FirstOrDefault(u => u.Email == email);
    }
}
