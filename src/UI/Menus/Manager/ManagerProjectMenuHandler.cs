using SmartIssueTrackingSystem.src.Application.Interfaces;

namespace SmartIssueTrackingSystem.src.UI.Menus.Manager
{
    public class ManagerProjectMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IAuthenticationService _authService;
        private readonly IProjectService _projectService;
        private readonly IProjectLifecycleService _projectLifecycle;

        public ManagerProjectMenuHandler(
            IAuthenticationService authService,
            IProjectService projectService,
            IProjectLifecycleService projectLifecycle)
        {
            _authService = authService;
            _projectService = projectService;
            _projectLifecycle = projectLifecycle;
        }

        public void Show()
        {
            while (true)
            {
                Clear();
                Console.WriteLine("1. Create project");
                Console.WriteLine("2. Rename project");
                Console.WriteLine("3. End project");
                Console.WriteLine("0. Back");

                switch (ReadChoice())
                {
                    case 1: CreateProject(); break;
                    case 2: RenameProject(); break;
                    case 3: EndProject(); break;
                    case 0: return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Pause();
                        break;
                }
            }
        }

        private void CreateProject()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? throw new ArgumentNullException("Name cannot be null.");

            Console.Write("Description: ");
            string description = Console.ReadLine() ?? throw new ArgumentNullException("Description cannot be null.");

            string managerEmail = _authService.GetCurrentUser().Email;

            _projectService.CreateProject(name, description, managerEmail);
            Console.WriteLine("Project created successfully.");

            Pause();
        }

        private void RenameProject()
        {
            Console.Write("Project ID: ");
            string input = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");

            if (!Guid.TryParse(input, out Guid projectId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Console.Write("New name: ");
            string newName = Console.ReadLine() ?? throw new ArgumentNullException("Name cannot be null.");

            var currentUser = _authService.GetCurrentUser();

            _projectService.RenameProject(projectId, newName, currentUser);
            Console.WriteLine("Project renamed successfully.");

            Pause();
        }

        private void EndProject()
        {
            Console.Write("Project ID: ");
            string input = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");

            if (!Guid.TryParse(input, out Guid projectId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            _projectLifecycle.EndProject(projectId, currentUser);
            Console.WriteLine("Project ended successfully.");

            Pause();
        }
    }
}
