using AutoMapper;
using Ddd.Dtos;
using MyProjects.Application.Dtos.Project;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Facade
{
    public class ProjectApplication(IReleasesRepository repository, IMapper mapper) : IProjectApplication
    {
        public async Task<IEnumerable<ProjectDto>> GetAll(int page = 1, int recordsPerPage = 10)
        {
            var pagination = new PaginationDto { Page = page, RecordsPerPage = recordsPerPage };

            var projects = await repository.GetAll(pagination);

            var projectDtos = mapper.Map<List<ProjectDto>>(projects);

            return await Task.FromResult(projectDtos);
        }

        public async Task<ProjectDto> GetById(string id)
        {
            var project = await repository.GetById(id);

            var projectDto = mapper.Map<ProjectDto>(project);

            return await Task.FromResult(projectDto);
        }

        public async Task<ProjectDto> Create(ProjectDto projectDto)
        {
            var project = mapper.Map<Release>(projectDto);

            await repository.Create(project);

            return await Task.FromResult(projectDto);
        }

        public async Task<ProjectDto> Update(string id, ProjectDto projectDto)
        {
            var project = mapper.Map<Release>(projectDto);

            await repository.Update(project);

            return await Task.FromResult(projectDto);
        }

        public async Task Delete(string id)
        {
            await repository.Delete(id);
        }
    }
}
