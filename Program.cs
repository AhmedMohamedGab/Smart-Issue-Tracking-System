using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Application.Services;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;
using SmartIssueTrackingSystem.src.Infrastructure.Repositories;
using SmartIssueTrackingSystem.src.UI;
using SmartIssueTrackingSystem.src.UI.Menus;
using SmartIssueTrackingSystem.src.UI.Menus.Admin;
using SmartIssueTrackingSystem.src.UI.Menus.Manager;

namespace SmartIssueTrackingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ---------- Infrastructure ----------
            IUserRepository userRepository = new UserRepository();
            IProjectRepository projectRepository = new ProjectRepository();
            IIssueRepository issueRepository = new IssueRepository();

            // ---------- Services ----------
            IUserService userService = new UserService(userRepository);

            IAuthenticationService authenticationService = new AuthenticationService(userService);
            IAuthorizationService authorizationService = new AuthorizationService();

            IProjectService projectService = new ProjectService(projectRepository, userService, authorizationService);
            IIssueService issueService = new IssueService(issueRepository, userService, projectService, authorizationService);
            IReportingService reportingService = new ReportingService(issueService, userService, projectService);

            IUserLifecycleService userLifecycle = new UserLifecycleService(userService, projectService, issueService);
            IProjectLifecycleService projectLifecycle = new ProjectLifecycleService(projectService, issueService);

            // ---------- Menus ----------
            // Auth menu
            AuthMenuHandler authMenu = new AuthMenuHandler(authenticationService, userService);

            // Admin menus
            AdminUserMenuHandler adminUserMenu = new AdminUserMenuHandler(
                authenticationService,
                userService,
                userLifecycle);

            AdminProjectMenuHandler adminProjectMenu = new AdminProjectMenuHandler(
                authenticationService,
                projectService,
                projectLifecycle);

            AdminMenuHandler adminMenu = new AdminMenuHandler(
                authenticationService,
                userService,
                adminUserMenu,
                adminProjectMenu);

            // Manager menus
            ManagerProjectMenuHandler managerProjectMenu = new ManagerProjectMenuHandler(
                authenticationService,
                projectService,
                projectLifecycle);

            ManagerIssueManagementMenuHandler managerIssueManagementMenu = new ManagerIssueManagementMenuHandler(
                authenticationService,
                issueService);

            ManagerIssueViewerMenuHandler managerIssueViewerMenu = new ManagerIssueViewerMenuHandler(
                authenticationService,
                issueService);

            ManagerReportMenuHandler managerReportMenu = new ManagerReportMenuHandler(
                authenticationService,
                reportingService);

            ManagerMenuHandler managerMenu = new ManagerMenuHandler(
                authenticationService,
                userService,
                managerProjectMenu,
                managerIssueManagementMenu,
                managerIssueViewerMenu,
                managerReportMenu);

            // Developer menu
            DeveloperMenuHandler developerMenu = new DeveloperMenuHandler(
                authenticationService,
                userService,
                issueService,
                reportingService);

            // ---------- Coordinator ----------
            MenuCoordinator menuCoordinator = new MenuCoordinator(
                authMenu,
                adminMenu,
                managerMenu,
                developerMenu,
                authenticationService);

            // ---------- App ----------
            ConsoleApp app = new ConsoleApp(menuCoordinator);
            app.Run();
        }
    }
}
