namespace SmartIssueTrackingSystem.src.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Guid ManagerId { get; private set; }

        public Project(string name, string description, Guid managerId)
        {
            Name = name;
            Description = description;
            ManagerId = managerId;

            StartDate = DateTime.UtcNow;
        }

        internal void Rename(string newName)
        {
            Name = newName;
        }

        internal void AssignTo(Guid managerId)
        {
            ManagerId = managerId;
        }

        internal void End()
        {
            EndDate = DateTime.UtcNow;
        }
    }
}
