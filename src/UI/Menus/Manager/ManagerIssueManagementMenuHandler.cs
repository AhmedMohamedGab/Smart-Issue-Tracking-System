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
            var title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Invalid title. Please try again.");
                Pause();
                return;
            }

            Console.Write("Description: ");
            var description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Invalid description. Please try again.");
                Pause();
                return;
            }

            Console.Write("Priority:\n1.Low\n2.Medium\n3.High\n4.Critical\n");
            int priority = ReadChoice();
            if (priority < 1 || priority > 4)
            {
                Console.WriteLine("Invalid priority choice.");
                Pause();
                return;
            }

            Console.Write("Due date (ex: 05/24/2026): ");
            var input1 = Console.ReadLine();
            if (!DateTime.TryParse(input1, out DateTime dueDate) || string.IsNullOrWhiteSpace(input1))
            {
                Console.WriteLine("Invalid date.");
                Pause();
                return;
            }

            Console.Write("Project ID: ");
            var input2 = Console.ReadLine();
            if (!Guid.TryParse(input2, out Guid projectId) || string.IsNullOrWhiteSpace(input2))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Guid managerId = _authService.GetCurrentUser().Id;

            _issueService.CreateIssue(title, description, priority, dueDate, managerId, projectId);
            Console.WriteLine("Issue created successfully!");

            Pause();
        }

        private void AssignIssue()
        {
            Console.Write("Issue ID: ");
            var input = Console.ReadLine();
            if (!Guid.TryParse(input, out Guid issueId) || string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Console.Write("Developer Email: ");
            var developerEmail = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(developerEmail))
            {
                Console.WriteLine("Invalid email. Please try again.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            try
            {
                _issueService.AssignIssue(issueId, developerEmail, currentUser);
                Console.WriteLine("Issue assigned successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        private void UnassignIssue()
        {
            Console.Write("Issue ID: ");
            var input = Console.ReadLine();
            if (!Guid.TryParse(input, out Guid issueId) || string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            try
            {
                _issueService.UnassignIssue(issueId, currentUser);
                Console.WriteLine("Issue unassigned successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        private void ChangeDuedate()
        {
            Console.Write("Issue ID: ");
            var input1 = Console.ReadLine();
            if (!Guid.TryParse(input1, out Guid issueId) || string.IsNullOrWhiteSpace(input1))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Console.Write("Due date (ex: 05/24/2026): ");
            var input2 = Console.ReadLine();
            if (!DateTime.TryParse(input2, out DateTime newDuedate) || string.IsNullOrWhiteSpace(input2))
            {
                Console.WriteLine("Invalid date.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            try
            {
                _issueService.ChangeDuedate(issueId, newDuedate, currentUser);
                Console.WriteLine("Due date changed successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        private void CloseIssue()
        {
            Console.Write("Issue ID: ");
            var input = Console.ReadLine();
            if (!Guid.TryParse(input, out Guid issueId) || string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            try
            {
                _issueService.CloseIssue(issueId, currentUser);
                Console.WriteLine("Issue closed successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        private void DeleteIssue()
        {
            Console.Write("Issue ID: ");
            var input = Console.ReadLine();
            if (!Guid.TryParse(input, out Guid issueId) || string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            try
            {
                _issueService.DeleteIssue(issueId, currentUser);
                Console.WriteLine("Issue deleted successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }
    }
}
