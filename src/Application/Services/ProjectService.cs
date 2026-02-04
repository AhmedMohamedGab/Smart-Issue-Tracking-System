using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IUserService _userService;
        private readonly IAuthorizationService _auth;

        public ProjectService(
            IProjectRepository projectRepository,
            IUserService userService,
            IAuthorizationService auth)
        {
            _projectRepo = projectRepository;
            _userService = userService;
            _auth = auth;
        }

        public void CreateProject(string name, string description, string ManagerEmail)
        {
            var manager = _userService.GetByEmail(ManagerEmail);

            var newProject = new Project(name, description, manager.Id);
            _projectRepo.Add(newProject);
        }

        public void RenameProject(Guid projectId, string newName, User currentUser)
        {
            var project = GetById(projectId);

            _auth.EnsureCanManageProject(project, currentUser);

            project.Rename(newName);
            _projectRepo.Update(project);
        }

        public void AssignManager(Guid projectId, Guid newManagerId, User currentUser)
        {
            var project = GetById(projectId);
            _userService.GetById(newManagerId); // Ensure the new manager exists

            _auth.EnsureCanManageProject(project, currentUser);

            project.AssignTo(newManagerId);
            _projectRepo.Update(project);
        }

        public void EndProject(Guid projectId, User currentUser)
        {
            var project = GetById(projectId);

            _auth.EnsureCanManageProject(project, currentUser);

            project.End();
            _projectRepo.Update(project);
        }

        public IEnumerable<Project> GetAllProjects()
            => _projectRepo.GetAll();

        public Project GetById(Guid projectId)
            => _projectRepo.GetById(projectId) ?? throw new InvalidOperationException("Project not found.");

        public IEnumerable<Project> GetProjectsManagedBy(Guid managerId)
            => _projectRepo.GetByManager(managerId);
    }
}
