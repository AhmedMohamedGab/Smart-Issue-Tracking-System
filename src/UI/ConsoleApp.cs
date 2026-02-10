namespace SmartIssueTrackingSystem.src.UI
{
    /// <summary>
    /// Represents the main entry point for the console-based application that coordinates menu-driven user interactions.
    /// </summary>
    public class ConsoleApp
    {
        private readonly MenuCoordinator _menuCoordinator;

        public ConsoleApp(MenuCoordinator menuCoordinator)
        {
            _menuCoordinator = menuCoordinator;
        }

        /// <summary>
        /// Starts the application's main menu loop and processes user interactions.
        /// </summary>
        /// <remarks>
        /// This method blocks until the menu loop is exited. It should be called once to begin
        /// the application's user interface flow.
        /// </remarks>
        public void Run()
        {
            _menuCoordinator.Run();
        }
    }
}
