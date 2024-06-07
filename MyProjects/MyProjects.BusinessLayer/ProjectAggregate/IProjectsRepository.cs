namespace MyProjects.Domain.ProjectAggregate
{
    public interface IProjectsRepository
    {
        Task<IEnumerable<Project>> GetAll();
        Task<Project?> GetById(string id);
        Task<string> Create(Project project);
        Task<bool> Exists(string id);
        Task Update(Project project);
        Task Delete(string id);
    }
}
