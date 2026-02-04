namespace SmartIssueTrackingSystem.src.UI
{
    public class ConsoleApp
    {
        private readonly MenuCoordinator _menuCoordinator;

        public ConsoleApp(MenuCoordinator menuCoordinator)
        {
            _menuCoordinator = menuCoordinator;
        }

        public void Run()
        {
            _menuCoordinator.Run();
        }
    }
}
