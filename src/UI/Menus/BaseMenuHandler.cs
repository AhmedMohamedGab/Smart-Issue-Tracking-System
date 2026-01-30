namespace SmartIssueTrackingSystem.src.UI.Menus
{
    public abstract class BaseMenuHandler
    {
        protected int ReadChoice()
        {
            Console.Write("Select option: ");
            int.TryParse(Console.ReadLine(), out int choice);
            return choice;
        }

        protected void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        protected void Clear()
        {
            Console.Clear();
        }
    }
}
