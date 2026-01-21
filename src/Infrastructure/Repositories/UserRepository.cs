using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Infrastructure.Repositories
{
    public class UserRepository : JsonRepository<User>, IUserRepository
    {
        protected override string FilePath => "users.json";

        public User? GetByEmail(string email)
            => _items.FirstOrDefault(u => u.Email == email);
    }
}
