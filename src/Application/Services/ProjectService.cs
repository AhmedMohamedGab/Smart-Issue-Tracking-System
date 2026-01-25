using SmartIssueTrackingSystem.src.Application.Interfaces;
using SmartIssueTrackingSystem.src.Domain.Entities;
using SmartIssueTrackingSystem.src.Infrastructure.Interfaces;

namespace SmartIssueTrackingSystem.src.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IUserRepository _userRepo;
        private readonly IAuthorizationService _auth;

        public ProjectService(
            IProjectRepository projectRepository,
            IUserRepository userRepository,
            IAuthorizationService auth)
        {
            _projectRepo = projectRepository;
            _userRepo = userRepository;
            _auth = auth;
        }

        public Project CreateProject(string name, string description, string ManagerEmail)
        {
            var manager = _userRepo.GetByEmail(ManagerEmail);
            if (manager is null)
                throw new InvalidOperationException("Manager with the specified email does not exist.");

            var newProject = new Project(name, description, manager.Id);
            _projectRepo.Add(newProject);

            return newProject;
        }

        public void RenameProject(Guid projectId, string newName, User currentUser)
        {
            var project = _projectRepo.GetById(projectId) ?? throw new InvalidOperationException("Project not found.");

            _auth.EnsureCanManageProject(project, currentUser);

            project.Rename(newName);
            _projectRepo.Update(project);
        }

        public IEnumerable<Project> GetAllProjects()
            => _projectRepo.GetAll();

        public Project? GetById(Guid projectId)
            => _projectRepo.GetById(projectId) ?? throw new InvalidOperationException("Project not found.");

        public IEnumerable<Project> GetProjectsManagedBy(Guid managerId)
            => _projectRepo.GetByManager(managerId);
    }
}
