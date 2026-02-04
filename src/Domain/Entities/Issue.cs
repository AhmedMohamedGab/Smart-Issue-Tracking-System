using SmartIssueTrackingSystem.src.Domain.Enums;

namespace SmartIssueTrackingSystem.src.Domain.Entities
{
    public class Issue : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IssueStatus Status { get; private set; }
        public IssuePriority Priority { get; private set; }
        public DateTime DueDate { get; private set; }

        public Guid ManagerId { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public Guid ProjectId { get; private set; }

        public DateTime? CompletedAt { get; private set; }

        public Issue(
            string title,
            string description,
            IssuePriority priority,
            DateTime dueDate,
            Guid managerId,
            Guid projectId)
        {
            Title = title;
            Description = description;
            Priority = priority;
            DueDate = dueDate;
            ManagerId = managerId;
            ProjectId = projectId;

            Status = IssueStatus.Open;
        }

        internal void AssignManager(Guid managerId)
        {
            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException("Cannot assign a closed issue.");

            ManagerId = managerId;
        }

        internal void AssignTo(Guid developerId)
        {
            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException("Cannot assign a closed issue.");

            AssigneeId = developerId;
            if (Status == IssueStatus.Open)
                Status = IssueStatus.InProgress;
        }

        internal void Unassign()
        {
            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException("Cannot unassign a closed issue.");

            AssigneeId = default;
            Status = IssueStatus.Open;
        }

        internal void ChangeStatus(IssueStatus newStatus)
        {
            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException("Closed issues cannot be changed.");

            Status = newStatus;

            if (newStatus == IssueStatus.Done)
                CompletedAt = DateTime.UtcNow;
        }

        internal void ChangeDueDate(DateTime newDueDate)
        {
            if (Status == IssueStatus.Done || Status == IssueStatus.Closed)
                throw new InvalidOperationException("Cannot set due date to a completed issue.");

            if (newDueDate < DateTime.UtcNow)
                throw new InvalidOperationException("Cannot set due date in the past.");

            DueDate = newDueDate;
        }

        internal bool IsOverdue()
        {
            if (DueDate < DateTime.UtcNow && !(Status == IssueStatus.Done || Status == IssueStatus.Closed))
                return true;

            return false;
        }
    }
}
