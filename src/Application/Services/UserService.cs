using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        public User CreateUser(string name, string email, UserRole role)
        {
            var newUser = new User(name, email, role);
            _userRepo.Add(newUser);

            return newUser;
        }

        public void EditInfo(string name, string email, User currentUser)
        {
            currentUser.EditInfo(name, email);
            _userRepo.Update(currentUser);
        }

        public IEnumerable<User> GetAllUsers()
            => _userRepo.GetAll();

        public User? GetById(Guid userId)
            => _userRepo.GetById(userId) ?? throw new InvalidOperationException("User not found.");

        public User? GetByEmail(string email)
            => _userRepo.GetByEmail(email) ?? throw new InvalidOperationException("User not found.");

        public IEnumerable<User> GetDevelopers()
            => _userRepo.GetAll()
            .Where(u => u.Role == UserRole.Developer);
    }
}
