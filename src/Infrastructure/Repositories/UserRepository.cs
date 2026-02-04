using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Infrastructure.Repositories
{
    public class UserRepository : JsonRepository<User>, IUserRepository
    {
        protected override string FilePath => "users.json";

        public User? GetByEmail(string email)
            => _items.FirstOrDefault(user => user.Email == email);

        public IEnumerable<User> GetDevelopers()
            => _items.Where(user => user.Role == UserRole.Developer);
    }
}
