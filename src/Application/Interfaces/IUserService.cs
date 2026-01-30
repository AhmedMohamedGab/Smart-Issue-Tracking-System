using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Application.Interfaces
{
    public interface IUserService
    {
        void CreateUser(string name, string email, int role);
        void EditInfo(string name, string email, User currentUser);
        void DeleteUser(Guid userId);

        IEnumerable<User> GetAllUsers();
        User GetById(Guid userId);
        User GetByEmail(string email);
        IEnumerable<User> GetDevelopers();
    }
}
