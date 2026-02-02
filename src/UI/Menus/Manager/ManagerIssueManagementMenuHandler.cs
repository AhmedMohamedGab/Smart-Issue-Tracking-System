using SmartIssueTrackingSystem.src.Application.Interfaces;

namespace SmartIssueTrackingSystem.src.UI.Menus.Manager
{
    public class ManagerIssueManagementMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IAuthenticationService _authService;
        private readonly IIssueService _issueService;

        public ManagerIssueManagementMenuHandler(IAuthenticationService authService, IIssueService issueService)
        {
            _authService = authService;
            _issueService = issueService;
        }

        public void Show()
        {
            while (true)
            {
                Clear();
                Console.WriteLine("1. Create issue");
                Console.WriteLine("2. Assign issue to developer");
                Console.WriteLine("3. Unassign issue");
                Console.WriteLine("4. Change due date");
                Console.WriteLine("5. Close issue");
                Console.WriteLine("6. Delete issue");
                Console.WriteLine("0. Back");

                switch (ReadChoice())
                {
                    case 1: CreateIssue(); break;
                    case 2: AssignIssue(); break;
                    case 3: UnassignIssue(); break;
                    case 4: ChangeDuedate(); break;
                    case 5: CloseIssue(); break;
                    case 6: DeleteIssue(); break;
                    case 0: return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Pause();
                        break;
                }
            }
        }

        private void CreateIssue()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine() ?? throw new ArgumentNullException("Title cannot be null.");

            Console.Write("Description: ");
            string description = Console.ReadLine() ?? throw new ArgumentNullException("Description cannot be null.");

            Console.Write("Priority:\n1.Low\n2.Medium\n3.High\n4.Critical\n");
            int priority = ReadChoice();
            if (priority < 1 || priority > 4)
            {
                Console.WriteLine("Invalid priority choice.");
                Pause();
                return;
            }

            Console.Write("Due date (ex: 24-5-2026): ");
            string input1 = Console.ReadLine() ?? throw new ArgumentNullException("Due date cannot be null.");
            if (!DateTime.TryParse(input1, out DateTime dueDate))
            {
                Console.WriteLine("Invalid date.");
                Pause();
                return;
            }

            Console.Write("Project ID: ");
            string input2 = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");
            if (!Guid.TryParse(input2, out Guid projectId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Guid managerId = _authService.GetCurrentUser().Id;

            _issueService.CreateIssue(title, description, priority, dueDate, managerId, projectId);
            Console.WriteLine("Issue created successfully.");

            Pause();
        }

        private void AssignIssue()
        {
            Console.Write("Issue ID: ");
            string input = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");
            if (!Guid.TryParse(input, out Guid issueId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Console.Write("Developer Email: ");
            string developerEmail = Console.ReadLine() ?? throw new ArgumentNullException("Email cannot be null.");

            var currentUser = _authService.GetCurrentUser();

            _issueService.AssignIssue(issueId, developerEmail, currentUser);
            Console.WriteLine("Issue assigned successfully.");

            Pause();
        }

        private void UnassignIssue()
        {
            Console.Write("Issue ID: ");
            string input = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");
            if (!Guid.TryParse(input, out Guid issueId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            _issueService.UnassignIssue(issueId, currentUser);
            Console.WriteLine("Issue unassigned successfully.");

            Pause();
        }

        private void ChangeDuedate()
        {
            Console.Write("Issue ID: ");
            string input1 = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");
            if (!Guid.TryParse(input1, out Guid issueId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Console.Write("Due date (ex: 24-5-2026): ");
            string input2 = Console.ReadLine() ?? throw new ArgumentNullException("Due date cannot be null.");
            if (!DateTime.TryParse(input2, out DateTime newDuedate))
            {
                Console.WriteLine("Invalid date.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            _issueService.ChangeDuedate(issueId, newDuedate, currentUser);
            Console.WriteLine("Due date changed successfully.");

            Pause();
        }

        private void CloseIssue()
        {
            Console.Write("Issue ID: ");
            string input = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");
            if (!Guid.TryParse(input, out Guid issueId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            _issueService.CloseIssue(issueId, currentUser);
            Console.WriteLine("Issue closed successfully.");

            Pause();
        }

        private void DeleteIssue()
        {
            Console.Write("Issue ID: ");
            string input = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");
            if (!Guid.TryParse(input, out Guid issueId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            _issueService.DeleteIssue(issueId, currentUser);
            Console.WriteLine("Issue deleted successfully.");

            Pause();
        }
    }
}
