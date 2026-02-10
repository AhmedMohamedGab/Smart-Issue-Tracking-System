namespace SmartIssueTrackingSystem.src.UI.Menus
{
    /// <summary>
    /// Defines a contract for displaying a menu in a user interface.
    /// </summary>
    public interface IMenuHandler
    {
        /// <summary>
        /// Displays the user interface associated with the current instance.
        /// </summary>
        /// <remarks>
        /// Implementations of this method should handle the logic for rendering the menu and processing user interactions.
        /// </remarks>
        void Show();
    }
}
