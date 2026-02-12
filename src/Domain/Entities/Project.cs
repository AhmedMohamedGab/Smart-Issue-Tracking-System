namespace SmartIssueTrackingSystem.src.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// The unique identifier of the manager associated with this project.
        /// </summary>
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
            if (EndDate is not null)
                throw new InvalidOperationException("Cannot reassign a project that has already ended.");
            ManagerId = managerId;
        }

        internal void End()
        {
            if (EndDate is not null)
                throw new InvalidOperationException("Project is already ended.");
            EndDate = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"\n- Name: {Name}\n" +
                $"\t- Description: {Description}\n" +
                $"\t- Project ID: {Id}\n" +
                $"\t- ManagerId: {ManagerId}\n" +
                $"\t- StartDate: {StartDate}\n" +
                $"\t- EndDate: {(EndDate.HasValue ? EndDate.Value.ToString() : "Ongoing")}";
        }
    }
}
