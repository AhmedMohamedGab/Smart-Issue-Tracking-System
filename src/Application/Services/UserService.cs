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

        public void CreateUser(string name, string email, int role)
        {
            // Check if user with the same email already exists
            var user = _userRepo.GetByEmail(email);
            if (user is not null)
                throw new InvalidOperationException("A user with this email already exists.");

            var newUser = new User(name, email, (UserRole)role);
            _userRepo.Add(newUser);
        }

        public void EditInfo(string name, string email, User currentUser)
        {
            currentUser.EditInfo(name, email);
            _userRepo.Update(currentUser);
        }

        public void DeleteUser(Guid userId)
        {
            GetById(userId);    // Ensure user exists
            _userRepo.Remove(userId);
        }

        public IEnumerable<User> GetAllUsers()
            => _userRepo.GetAll();

        public User GetById(Guid userId)
            => _userRepo.GetById(userId) ?? throw new InvalidOperationException("User not found.");

        public User GetByEmail(string email)
            => _userRepo.GetByEmail(email) ?? throw new InvalidOperationException("User not found.");

        public IEnumerable<User> GetDevelopers()
            => _userRepo.GetDevelopers();
    }
}
