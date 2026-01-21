namespace SmartIssueTrackingSystem.src.Domain.Entities
{
    public abstract class ProjectCreatingUser : User
    {
        protected ProjectCreatingUser(string name, string email) : base(name, email)
        {
        }
    }
}
