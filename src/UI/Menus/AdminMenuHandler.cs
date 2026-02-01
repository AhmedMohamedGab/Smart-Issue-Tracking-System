using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.UI.Menus.Admin;

namespace SmartIssueTrackingSystem.src.UI.Menus
{
    public class AdminMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly AdminUserMenuHandler _userMenu;
        private readonly AdminProjectMenuHandler _projectMenu;
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;

        public AdminMenuHandler(
            AdminUserMenuHandler userMenu,
            AdminProjectMenuHandler projectMenu,
            IAuthenticationService authService,
            IUserService userService)
        {
            _userMenu = userMenu;
            _projectMenu = projectMenu;
            _authService = authService;
            _userService = userService;
        }

        public void Show()
        {
            while (true)
            {
                Clear();
                Console.WriteLine("1. Edit my info");
                Console.WriteLine("2. Manage users");
                Console.WriteLine("3. Manage projects");
                Console.WriteLine("0. Logout");

                switch (ReadChoice())
                {
                    case 1: EditInfo(); break;
                    case 2: _userMenu.Show(); break;
                    case 3: _projectMenu.Show(); break;
                    case 0: _authService.Logout(); break;
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
            string name = Console.ReadLine() ?? throw new ArgumentNullException("Name cannot be null.");

            Console.Write("Enter new email: ");
            string email = Console.ReadLine() ?? throw new ArgumentNullException("Email cannot be null.");

            _userService.EditInfo(name, email, currentUser);
            Console.WriteLine("Info updated successfully.");

            Pause();
        }
    }
}
