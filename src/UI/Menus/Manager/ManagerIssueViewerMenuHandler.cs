using SmartIssueTrackingSystem.src.Application.Interfaces;

namespace SmartIssueTrackingSystem.src.UI.Menus.Manager
{
    public class ManagerIssueViewerMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IAuthenticationService _authService;
        private readonly IIssueService _issueService;

        public ManagerIssueViewerMenuHandler(IAuthenticationService authService, IIssueService issueService)
        {
            _authService = authService;
            _issueService = issueService;
        }

        public void Show()
        {
            while (true)
            {
                Clear();
                Console.WriteLine("1. Group by project");
                Console.WriteLine("2. Group by assigned developer");
                Console.WriteLine("3. Group by status");
                Console.WriteLine("4. Group by priority");
                Console.WriteLine("5. Get incomplete issues");
                Console.WriteLine("6. Get overdue issues");
                Console.WriteLine("0. Back");

                switch (ReadChoice())
                {
                    case 1: GroupByProject(); break;
                    case 2: GroupByAsignee(); break;
                    case 3: GroupByStatus(); break;
                    case 4: GroupByPriority(); break;
                    case 5: GetIncompleteIssues(); break;
                    case 6: GetOverdueIssues(); break;
                    case 0: return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Pause();
                        break;
                }
            }
        }

        private void GroupByProject()
        {
            Guid managerId = _authService.GetCurrentUser().Id;
            var issueDictionary = _issueService.GetForManager(managerId);

            foreach (var item in issueDictionary)
            {
                Console.WriteLine($"\n# Issues for project [{item.Key}] :");
                foreach (var issue in item.Value)
                    Console.WriteLine(issue);
            }

            if (issueDictionary.Count == 0)
                Console.WriteLine("\nNo issues found.");

            Pause();
        }

        private void GroupByAsignee()
        {
            Guid managerId = _authService.GetCurrentUser().Id;
            var issueDictionary = _issueService.GetByAssignee(managerId);

            foreach (var item in issueDictionary)
            {
                Console.WriteLine($"\n# Issues for developer [{item.Key}] :");
                foreach (var issue in item.Value)
                    Console.WriteLine(issue);
            }

            if (issueDictionary.Count == 0)
                Console.WriteLine("\nNo issues assigned.");

            Pause();
        }

        private void GroupByStatus()
        {
            Guid managerId = _authService.GetCurrentUser().Id;
            var issueDictionary = _issueService.GetByStatus(managerId);

            foreach (var item in issueDictionary)
            {
                Console.WriteLine($"\n# {item.Key} issues :");
                foreach (var issue in item.Value)
                    Console.WriteLine(issue);
            }

            if (issueDictionary.Count == 0)
                Console.WriteLine("\nNo issues found.");

            Pause();
        }

        private void GroupByPriority()
        {
            Guid managerId = _authService.GetCurrentUser().Id;
            var issueDictionary = _issueService.GetByPriority(managerId);

            foreach (var item in issueDictionary)
            {
                Console.WriteLine($"\n# {item.Key} priority issues :");
                foreach (var issue in item.Value)
                    Console.WriteLine(issue);
            }

            if (issueDictionary.Count == 0)
                Console.WriteLine("\nNo issues found.");

            Pause();
        }

        private void GetIncompleteIssues()
        {
            Guid managerId = _authService.GetCurrentUser().Id;
            var incompleteIssues = _issueService.GetIncompleteIssues(managerId);

            Console.WriteLine("\n# Incomplete issues :");
            foreach (var issue in incompleteIssues)
                Console.WriteLine(issue);

            if (incompleteIssues.Count() == 0)
                Console.WriteLine("\nNo incomplete issues found.");

            Pause();
        }

        private void GetOverdueIssues()
        {
            Guid managerId = _authService.GetCurrentUser().Id;
            var overdueIssues = _issueService.GetOverdueIssues(managerId);

            Console.WriteLine("\n# Overdue issues :");
            foreach (var issue in overdueIssues)
                Console.WriteLine(issue);

            if (overdueIssues.Count() == 0)
                Console.WriteLine("\nNo overdue issues found.");

            Pause();
        }
    }
}
