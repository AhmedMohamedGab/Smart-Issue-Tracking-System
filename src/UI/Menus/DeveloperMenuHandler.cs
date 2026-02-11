using SmartIssueTrackingSystem.src.Application.Interfaces;

namespace SmartIssueTrackingSystem.src.UI.Menus
{
    public class DeveloperMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly IIssueService _issueService;
        private readonly IReportingService _reportingService;

        public DeveloperMenuHandler(
            IAuthenticationService authService,
            IUserService userService,
            IIssueService issueService,
            IReportingService reportingService)
        {
            _authService = authService;
            _userService = userService;
            _issueService = issueService;
            _reportingService = reportingService;
        }

        public void Show()
        {
            while (true)
            {
                Clear();
                Console.WriteLine("1. Edit my info");
                Console.WriteLine("2. View my issues");
                Console.WriteLine("3. Change issue status");
                Console.WriteLine("4. View my workload");
                Console.WriteLine("0. Logout");

                switch (ReadChoice())
                {
                    case 1: EditInfo(); break;
                    case 2: ViewIssues(); break;
                    case 3: ChangeIssueStatus(); break;
                    case 4: ViewWorkload(); break;
                    case 0: _authService.Logout(); return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Pause();
                        break;
                }
            }
        }

        private void EditInfo()
        {
            var currentUser = _authService.GetCurrentUser();

            Console.WriteLine($"Name: {currentUser.Name}");
            Console.WriteLine($"Email: {currentUser.Email}");
            Console.WriteLine("---------------------");

            Console.Write("Enter new name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name. Please try again.");
                Pause();
                return;
            }

            Console.Write("Enter new email: ");
            var email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Invalid email. Please try again.");
                Pause();
                return;
            }

            _userService.EditInfo(name, email, currentUser);
            Console.WriteLine("Info updated successfully!");

            Pause();
        }

        private void ViewIssues()
        {
            Guid developerId = _authService.GetCurrentUser().Id;
            var issueDictionary = _issueService.GetForDeveloper(developerId);

            foreach (var item in issueDictionary)
            {
                Console.WriteLine($"\n# Issues for project [{item.Key}] :");
                foreach (var issue in item.Value)
                    Console.WriteLine(issue);
            }

            if (issueDictionary.Count == 0)
                Console.WriteLine("\nNo issues assigned.");

            Pause();
        }

        private void ChangeIssueStatus()
        {
            Console.Write("Enter issue ID: ");
            var input = Console.ReadLine();

            if (!Guid.TryParse(input, out Guid issueId) || string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Console.Write("New status:\n1.In progress\n2.In review\n3.Done\n");
            int newStatus = ReadChoice();
            if (newStatus < 1 || newStatus > 3)
            {
                Console.WriteLine("Invalid status choice.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            try
            {
                _issueService.ChangeStatus(issueId, newStatus, currentUser);
                Console.WriteLine("Issue status updated successfully!");
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

        private void ViewWorkload()
        {
            Guid developerId = _authService.GetCurrentUser().Id;

            int workload = _reportingService.GetDeveloperWorkload(developerId);
            Console.WriteLine($"You have {workload} issues assigned.");

            Pause();
        }
    }
}
