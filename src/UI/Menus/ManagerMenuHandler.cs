using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.UI.Menus.Manager;

namespace SmartIssueTrackingSystem.src.UI.Menus
{
    public class ManagerMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly ManagerProjectMenuHandler _projectMenu;
        private readonly ManagerIssueManagementMenuHandler _manageIssuesMenu;
        private readonly ManagerIssueViewerMenuHandler _viewIssuesMenu;
        private readonly ManagerReportMenuHandler _reportsMenu;

        public ManagerMenuHandler(
            IAuthenticationService authService,
            IUserService userService,
            ManagerProjectMenuHandler projectMenu,
            ManagerIssueManagementMenuHandler manageIssuesMenu,
            ManagerIssueViewerMenuHandler viewIssuesMenu,
            ManagerReportMenuHandler reportsMenu)
        {
            _authService = authService;
            _userService = userService;
            _projectMenu = projectMenu;
            _manageIssuesMenu = manageIssuesMenu;
            _viewIssuesMenu = viewIssuesMenu;
            _reportsMenu = reportsMenu;
        }

        public void Show()
        {
            while (true)
            {
                Clear();
                Console.WriteLine("1. Edit my info");
                Console.WriteLine("2. Projects >");
                Console.WriteLine("3. Manage issues >");
                Console.WriteLine("4. View issues >");
                Console.WriteLine("5. Reports >");
                Console.WriteLine("0. Logout");

                switch (ReadChoice())
                {
                    case 1: EditInfo(); break;
                    case 2: _projectMenu.Show(); break;
                    case 3: _manageIssuesMenu.Show(); break;
                    case 4: _viewIssuesMenu.Show(); break;
                    case 5: _reportsMenu.Show(); break;
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
    }
}
