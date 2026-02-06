using SmartIssueTrackingSystem.src.Application.Interfaces;

namespace SmartIssueTrackingSystem.src.UI.Menus.Admin
{
    public class AdminUserMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly IUserLifecycleService _userLifecycle;

        public AdminUserMenuHandler(
            IAuthenticationService authService,
            IUserService userService,
            IUserLifecycleService userLifecycle)
        {
            _authService = authService;
            _userService = userService;
            _userLifecycle = userLifecycle;
        }

        public void Show()
        {
            while (true)
            {
                Clear();
                Console.WriteLine("1. Create user");
                Console.WriteLine("2. Delete user");
                Console.WriteLine("3. Get all users");
                Console.WriteLine("4. Get user by email");
                Console.WriteLine("0. Back");

                switch (ReadChoice())
                {
                    case 1: CreateUser(); break;
                    case 2: DeleteUser(); break;
                    case 3: GetAllUsers(); break;
                    case 4: GetUserByEmail(); break;
                    case 0: return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Pause();
                        break;
                }
            }
        }

        private void CreateUser()
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name. Please try again.");
                Pause();
                return;
            }

            Console.Write("Email: ");
            var email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Invalid email. Please try again.");
                Pause();
                return;
            }

            Console.Write("Role:\n1.Admin\n2.Manager\n3.Developer\n");
            int role = ReadChoice();
            if (role < 1 || role > 3)
            {
                Console.WriteLine("Invalid role choice.");
                Pause();
                return;
            }

            try
            {
                _userService.CreateUser(name, email, role);
                Console.WriteLine("User created successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        private void DeleteUser()
        {
            Console.Write("Role:\n1.Admin\n2.Manager\n3.Developer\n");
            int role = ReadChoice();
            if (role < 1 || role > 3)
            {
                Console.WriteLine("Invalid role choice.");
                Pause();
                return;
            }

            Console.Write("User ID: ");
            var input1 = Console.ReadLine();

            if (!Guid.TryParse(input1, out Guid userId) || string.IsNullOrWhiteSpace(input1))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Guid newManagerId = Guid.Empty;
            if (role == 2)
            {
                Console.Write("Replacing manager ID: ");
                var input2 = Console.ReadLine();

                if (!Guid.TryParse(input2, out newManagerId) || string.IsNullOrWhiteSpace(input2))
                {
                    Console.WriteLine("Invalid ID.");
                    Pause();
                    return;
                }
            }

            var currentUser = _authService.GetCurrentUser();

            try
            {
                _userLifecycle.DeleteUser(userId, newManagerId, currentUser);
                Console.WriteLine("User deleted successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        private void GetAllUsers()
        {
            var allUsers = _userService.GetAllUsers();

            foreach (var user in allUsers)
                Console.WriteLine(user);

            Pause();
        }

        private void GetUserByEmail()
        {
            Console.Write("Email: ");
            var email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Invalid email. Please try again.");
                Pause();
                return;
            }

            try
            {
                var user = _userService.GetByEmail(email);
                Console.WriteLine(user);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }
    }
}
