using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class UserService : IUserService
    {
        public User CreateUser(string name, string email, UserRole role, User currentUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetDevelopers()
        {
            throw new NotImplementedException();
        }
    }
}
