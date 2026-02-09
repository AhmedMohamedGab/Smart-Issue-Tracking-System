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

        public Guid ManagerId { get; private set; } // The manager responsible for overseeing the issue
        public Guid? AssigneeId { get; private set; }   // The developer assigned to work on the issue (nullable, as it may be unassigned)
        public Guid ProjectId { get; private set; } // The project this issue belongs to

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

            Status = IssueStatus.Open;  // No assignee yet, so start as Open
        }

        internal void AssignManager(Guid managerId)
        {
            // Only allow changing manager if issue is not closed
            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException("Cannot assign a closed issue.");

            ManagerId = managerId;
        }

        internal void AssignTo(Guid developerId)
        {
            // Only allow assigning a developer if issue is not closed
            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException("Cannot assign a closed issue.");

            AssigneeId = developerId;

            // If issue was previously unassigned (open), move it to InProgress
            if (Status == IssueStatus.Open)
                Status = IssueStatus.InProgress;
        }

        internal void Unassign()
        {
            // Only allow unassigning if issue is not closed
            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException("Cannot unassign a closed issue.");

            AssigneeId = default;
            Status = IssueStatus.Open;  // Move back to Open to indicate it's unassigned
        }

        internal void ChangeStatus(IssueStatus newStatus)
        {
            // Only allow status changes if issue is not closed
            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException("Closed issues cannot be changed.");

            Status = newStatus;

            // If issue is marked as Done, set the CompletedAt timestamp
            if (newStatus == IssueStatus.Done)
                CompletedAt = DateTime.UtcNow;
        }

        internal void ChangeDueDate(DateTime newDueDate)
        {
            // Only allow changing due date if issue is not closed or done
            if (Status == IssueStatus.Done || Status == IssueStatus.Closed)
                throw new InvalidOperationException("Cannot set due date to a completed issue.");

            // Prevent setting due date in the past
            if (newDueDate < DateTime.UtcNow)
                throw new InvalidOperationException("Cannot set due date in the past.");

            DueDate = newDueDate;
        }

        internal bool IsOverdue()
        {
            // An issue is considered overdue if the current date is past the due date and the issue is not done or closed
            if (DueDate < DateTime.UtcNow && !(Status == IssueStatus.Done || Status == IssueStatus.Closed))
                return true;

            return false;
        }
    }
}
