using SmartIssueTrackingSystem.src.Application.Interfaces;

namespace SmartIssueTrackingSystem.src.UI.Menus
{
    public class AuthMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;

        public AuthMenuHandler(IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public void Show()
        {
            // Handle running system for the first time
            if (!_userService.GetAllUsers().Any())
            {
                string name = "System Admin";
                string email = "admin@system.com";
                int role = 1;   // Admin

                _userService.CreateUser(name, email, role);
                _authService.Login(email);  // Admin is authenticated now

                Console.WriteLine("Welcome for the first time!");
                Console.WriteLine("Yor are the administrator of this system.");
                Console.WriteLine("Here's your initial info that you can change later:");
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Email: {email}");

                Pause();
            }

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
            Console.Write("Enter your email: ");
            var email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Invalid email. Please try again.");
                Pause();
                return;
            }

            try
            {
                _authService.Login(email);
                Console.WriteLine("You logged in successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }
    }
}
