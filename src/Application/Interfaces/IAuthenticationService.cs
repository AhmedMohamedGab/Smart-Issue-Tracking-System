using SmartIssueTrackingSystem.src.Domain.Entities;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IAuthenticationService
    {
        User Login(string email);
        void Logout();
        bool IsAuthenticated();
        User GetCurrentUser();
    }
}
