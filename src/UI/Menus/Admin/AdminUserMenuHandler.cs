using SmartIssueTrackingSystem.src.Application.Interfaces;

namespace SmartIssueTrackingSystem.src.UI.Menus.Admin
{
    public class AdminUserMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IUserService _userService;
        private readonly IUserLifecycleService _userLifecycle;
        private readonly IAuthenticationService _authService;

        public AdminUserMenuHandler(
            IUserService userService,
            IUserLifecycleService userLifecycle,
            IAuthenticationService authService)
        {
            _userService = userService;
            _userLifecycle = userLifecycle;
            _authService = authService;
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
            string name = Console.ReadLine() ?? throw new ArgumentNullException("Name cannot be null.");

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? throw new ArgumentNullException("Email cannot be null.");

            Console.Write("Role:\n1.Admin\n2.Manager\n3.Developer\n");
            int role = ReadChoice();
            if (role < 1 || role > 3)
            {
                Console.WriteLine("Invalid role choice.");
                Pause();
                return;
            }

            _userService.CreateUser(name, email, role);
            Console.WriteLine("User created successfully.");

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
            string input1 = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");

            if (!Guid.TryParse(input1, out Guid id1))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Guid id2 = Guid.Empty;
            if (role == 2)
            {
                Console.Write("Replacing manager ID: ");
                string input2 = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");

                if (!Guid.TryParse(input2, out id2))
                {
                    Console.WriteLine("Invalid ID.");
                    Pause();
                    return;
                }
            }

            var currentUser = _authService.GetCurrentUser();

            _userLifecycle.DeleteUser(id1, id2, currentUser);
            Console.WriteLine("User deleted successfully.");

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
            string email = Console.ReadLine() ?? throw new ArgumentNullException("Email cannot be null.");

            var user = _userService.GetByEmail(email);
            Console.WriteLine(user.ToString() ?? "User not found.");
            Pause();
        }
    }
}
