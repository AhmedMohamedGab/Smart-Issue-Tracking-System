using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Enums;
using SmartIssueTrackingSystem.src.UI.Menus;

namespace SmartIssueTrackingSystem.src.UI
{
    public class MenuCoordinator
    {
        private readonly AuthMenuHandler _authMenu;
        private readonly AdminMenuHandler _adminMenu;
        private readonly ManagerMenuHandler _managerMenu;
        private readonly DeveloperMenuHandler _developerMenu;
        private readonly IAuthenticationService _authService;

        public MenuCoordinator(
            AuthMenuHandler authMenu,
            AdminMenuHandler adminMenu,
            ManagerMenuHandler managerMenu,
            DeveloperMenuHandler developerMenu,
            IAuthenticationService authService)
        {
            _authMenu = authMenu;
            _adminMenu = adminMenu;
            _managerMenu = managerMenu;
            _developerMenu = developerMenu;
            _authService = authService;
        }

        public void Run()
        {
            while (true)
            {
                if (!_authService.IsAuthenticated())
                    _authMenu.Show();
                else
                    RouteByRole();
            }
        }

        private void RouteByRole()
        {
            switch (_authService.GetCurrentUser().Role)
            {
                case UserRole.Admin:
                    _adminMenu.Show();
                    break;

                case UserRole.Manager:
                    _managerMenu.Show();
                    break;

                case UserRole.Developer:
                    _developerMenu.Show();
                    break;
            }
        }
    }
}
