namespace SmartIssueTrackingSystem.src.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Guid CreatorId { get; private set; }

        public Project(string name, string description, Guid creatorId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            CreatorId = creatorId;

            StartDate = DateTime.UtcNow;
        }

        internal void End()
        {
            EndDate = DateTime.UtcNow;
        }
    }
}
