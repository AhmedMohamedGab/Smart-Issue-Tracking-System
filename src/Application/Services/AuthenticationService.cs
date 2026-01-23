using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private User? _currentUser;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Login(string email)
        {
            var user = _userRepository.GetByEmail(email);

            if (user is null)
                throw new Exception("User not found.");

            _currentUser = user;
            return user;
        }

        public void Logout()
            => _currentUser = null;

        public User GetCurrentUser()
            => _currentUser ?? throw new Exception("No user is currently logged in.");

        public bool IsAuthenticated()
            => _currentUser is not null;
    }
}
