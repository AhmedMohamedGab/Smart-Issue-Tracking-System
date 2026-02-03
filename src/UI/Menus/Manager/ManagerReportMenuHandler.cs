using SmartIssueTrackingSystem.src.Application.Interfaces;

namespace SmartIssueTrackingSystem.src.UI.Menus.Manager
{
    public class ManagerReportMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IAuthenticationService _authService;
        private readonly IReportingService _reportingService;

        public ManagerReportMenuHandler(IAuthenticationService authService, IReportingService reportingService)
        {
            _authService = authService;
            _reportingService = reportingService;
        }

        public void Show()
        {
            while (true)
            {
                Clear();
                Console.WriteLine("1. Open issue count for project");
                Console.WriteLine("2. Overdue issue count for project");
                Console.WriteLine("3. Issue count by status");
                Console.WriteLine("4. Project progress");
                Console.WriteLine("0. Back");

                switch (ReadChoice())
                {
                    case 1: OpenIssueCount(); break;
                    case 2: OverdueIssueCount(); break;
                    case 3: IssueCountByStatus(); break;
                    case 4: ProjectProgress(); break;
                    case 0: return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Pause();
                        break;
                }
            }
        }

        private void OpenIssueCount()
        {
            Console.WriteLine("Project ID: ");
            string input = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");
            if (!Guid.TryParse(input, out Guid projectId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            int openIssueCount = _reportingService.GetOpenIssueCountForProject(projectId, currentUser);
            Console.WriteLine($"\nOpen issues for project [{projectId}]: {openIssueCount}");

            Pause();
        }

        private void OverdueIssueCount()
        {
            Console.WriteLine("Project ID: ");
            string input = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");
            if (!Guid.TryParse(input, out Guid projectId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            int overdueIssueCount = _reportingService.GetOverdueIssueCountForProject(projectId, currentUser);
            Console.WriteLine($"\nOverdue issues for project [{projectId}]: {overdueIssueCount}");

            Pause();
        }

        private void IssueCountByStatus()
        {
            Console.WriteLine("Project ID: ");
            string input = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");
            if (!Guid.TryParse(input, out Guid projectId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            var issueCountByStatus = _reportingService.GetIssueCountByStatus(projectId, currentUser);
            foreach (var statusCount in issueCountByStatus)
                Console.WriteLine($"Status: {statusCount.Key}, Count: {statusCount.Value}");

            Pause();
        }

        private void ProjectProgress()
        {
            Console.WriteLine("Project ID: ");
            string input = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");
            if (!Guid.TryParse(input, out Guid projectId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            double projectProgress = _reportingService.GetProjectProgress(projectId, currentUser);
            Console.WriteLine($"\nProject progress for project [{projectId}]: {projectProgress:F2}%");

            Pause();
        }
    }
}
