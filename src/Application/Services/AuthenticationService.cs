using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private User? _currentUser;
        private readonly IUserService _userService;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        public void Login(string email)
        {
            var user = _userService.GetByEmail(email);

            if (user is null)
                throw new Exception("User not found.");

            _currentUser = user;
        }

        public void Logout()
            => _currentUser = null;

        public User GetCurrentUser()
            => _currentUser ?? throw new Exception("No user is currently logged in.");

        public bool IsAuthenticated()
            => _currentUser is not null;
    }
}
