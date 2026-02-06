using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.UI.Menus.Admin;

namespace SmartIssueTrackingSystem.src.UI.Menus
{
    public class AdminMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly AdminUserMenuHandler _userMenu;
        private readonly AdminProjectMenuHandler _projectMenu;

        public AdminMenuHandler(
            IAuthenticationService authService,
            IUserService userService,
            AdminUserMenuHandler userMenu,
            AdminProjectMenuHandler projectMenu)
        {
            _authService = authService;
            _userService = userService;
            _userMenu = userMenu;
            _projectMenu = projectMenu;
        }

        public void Show()
        {
            while (true)
            {
                Clear();
                Console.WriteLine("1. Edit my info");
                Console.WriteLine("2. Users >");
                Console.WriteLine("3. Projects >");
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
