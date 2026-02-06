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
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name. Please try again.");
                Pause();
                return;
            }

            Console.Write("Description: ");
            var description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Invalid description. Please try again.");
                Pause();
                return;
            }

            Console.Write("Manager email: ");
            var managerEmail = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(managerEmail))
            {
                Console.WriteLine("Invalid email. Please try again.");
                Pause();
                return;
            }

            try
            {
                _projectService.CreateProject(name, description, managerEmail);
                Console.WriteLine("Project created successfully!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        private void ReassignProject()
        {
            Console.Write("Project ID: ");
            var input1 = Console.ReadLine();

            if (!Guid.TryParse(input1, out Guid projectId) || string.IsNullOrWhiteSpace(input1))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Console.Write("New manager ID: ");
            var input2 = Console.ReadLine();

            if (!Guid.TryParse(input2, out Guid newManagerId) || string.IsNullOrWhiteSpace(input2))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            try
            {
                _projectLifecycle.ReassignProject(projectId, newManagerId, currentUser);
                Console.WriteLine("Project reassigned successfully!");
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

        private void GetAllProjects()
        {
            var allProjects = _projectService.GetAllProjects();

            foreach (var project in allProjects)
                Console.WriteLine(project);

            if (allProjects.Count() == 0)
                Console.WriteLine("\nNo projects found.");

            Pause();
        }
    }
}
