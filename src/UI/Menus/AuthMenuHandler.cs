using SmartIssueTrackingSystem.src.Application.Interfaces;

namespace SmartIssueTrackingSystem.src.UI.Menus
{
    public class AuthMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IAuthenticationService _authService;

        public AuthMenuHandler(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public void Show()
        {
            while (!_authService.IsAuthenticated())
            {
                Clear();
                Console.WriteLine("1. Login");
                Console.WriteLine("0. Exit");

                switch (ReadChoice())
                {
                    case 1:
                        Login();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void Login()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? throw new ArgumentNullException("Email cannot be null.");

            _authService.Login(email);
        }
    }
}
