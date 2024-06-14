using MyProjects.Application.Dtos.Project;

namespace MyProjects.Application.Facade
{
    public interface IProjectApplication
    {
        Task<IEnumerable<ProjectDto>> GetAll(int page = 1, int recordsPerPage = 10);
        Task<ProjectDto> GetById(string id);
        Task<ProjectDto> Create(ProjectDto projectDto);
        Task<ProjectDto> Update(string id, ProjectDto projectDto);
        Task Delete(string id);
        
    }
}