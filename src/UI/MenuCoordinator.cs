using SmartIssueTrackingSystem.src.Application.Interfaces;
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

        /// <summary>
        /// Runs the main application loop, displaying authentication or routing menus based on the user's
        /// authentication status.
        /// </summary>
        /// <remarks>
        /// This method blocks the calling thread and does not return. It repeatedly checks the user's authentication
        /// state and displays the appropriate menu. To stop the loop, the application must be terminated externally.
        /// </remarks>
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

        /// <summary>
        /// Routes the current user to the appropriate menu based on their role.
        /// </summary>
        /// <remarks>
        /// This method determines the user's role and displays the corresponding menu. Only one menu is shown
        /// per invocation.
        /// </remarks>
        private void RouteByRole()
        {
            switch (_authService.GetCurrentUser().Role.ToString())
            {
                case "Admin":
                    _adminMenu.Show();
                    break;

                case "Manager":
                    _managerMenu.Show();
                    break;

                case "Developer":
                    _developerMenu.Show();
                    break;
            }
        }
    }
}
