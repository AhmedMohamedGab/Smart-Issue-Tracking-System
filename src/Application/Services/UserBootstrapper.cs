using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class UserBootstrapper : IUserBootstrapper
    {
        private readonly IUserRepository _userRepository;

        public UserBootstrapper(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Initialize()
        {
            if (_userRepository.GetAll().Any())
                return;

            _userRepository.Add(new User("System Admin", "admin@system.com", UserRole.Admin));
        }
    }
}
