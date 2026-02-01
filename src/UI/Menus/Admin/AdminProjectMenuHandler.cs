using SmartIssueTrackingSystem.src.Application.Interfaces;

namespace SmartIssueTrackingSystem.src.UI.Menus.Admin
{
    public class AdminProjectMenuHandler : BaseMenuHandler, IMenuHandler
    {
        private readonly IAuthenticationService _authService;
        private readonly IProjectService _projectService;
        private readonly IProjectLifecycleService _projectLifecycle;

        public AdminProjectMenuHandler(
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
                Console.WriteLine("2. Reassign project to new manager");
                Console.WriteLine("3. Get all projects");
                Console.WriteLine("0. Back");

                switch (ReadChoice())
                {
                    case 1: CreateProject(); break;
                    case 2: ReassignProject(); break;
                    case 3: GetAllProjects(); break;
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

            Console.Write("Manager email: ");
            string managerEmail = Console.ReadLine() ?? throw new ArgumentNullException("Email cannot be null.");

            _projectService.CreateProject(name, description, managerEmail);
            Console.WriteLine("Project created successfully.");

            Pause();
        }

        private void ReassignProject()
        {
            Console.Write("Project ID: ");
            string input1 = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");

            if (!Guid.TryParse(input1, out Guid projectId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Console.Write("New manager ID: ");
            string input2 = Console.ReadLine() ?? throw new ArgumentNullException("ID cannot be null.");

            if (!Guid.TryParse(input2, out Guid newManagerId))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            _projectLifecycle.ReassignProject(projectId, newManagerId, currentUser);
            Console.WriteLine("Project reassigned successfully.");

            Pause();
        }

        private void GetAllProjects()
        {
            var allProjects = _projectService.GetAllProjects();

            foreach (var project in allProjects)
                Console.WriteLine(project);

            Pause();
        }
    }
}
