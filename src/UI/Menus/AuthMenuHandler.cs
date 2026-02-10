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

        /// <summary>
        /// Displays the main entry point for the user interface, handling initial system setup and user authentication.
        /// </summary>
        /// <remarks>
        /// If no users exist in the system, this method creates a default administrator account
        /// and prompts the user with initial credentials. The method then repeatedly prompts for user login until
        /// authentication is successful or the application is exited.
        /// </remarks>
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

        /// <summary>
        /// Attempts to log in a user by prompting for an email address and invoking the authentication service.
        /// </summary>
        /// <remarks>
        /// Displays a prompt for the user's email address and validates the input. If the email is valid,
        /// calls the authentication service to perform the login. Provides feedback to the user based on the
        /// outcome of the login attempt.
        /// </remarks>
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
