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

            string managerEmail = _authService.GetCurrentUser().Email;

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

        private void RenameProject()
        {
            Console.Write("Project ID: ");
            var input = Console.ReadLine();

            if (!Guid.TryParse(input, out Guid projectId) || string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Console.Write("New name: ");
            var newName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Invalid name. Please try again.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            try
            {
                _projectService.RenameProject(projectId, newName, currentUser);
                Console.WriteLine("Project renamed successfully!");
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

        private void EndProject()
        {
            Console.Write("Project ID: ");
            var input = Console.ReadLine();

            if (!Guid.TryParse(input, out Guid projectId) || string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            var currentUser = _authService.GetCurrentUser();

            try
            {
                _projectLifecycle.EndProject(projectId, currentUser);
                Console.WriteLine("Project ended successfully!");
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
    }
}
