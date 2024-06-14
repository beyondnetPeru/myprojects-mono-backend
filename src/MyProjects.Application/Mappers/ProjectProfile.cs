using AutoMapper;
using MyProjects.Application.Dtos.Project;
using MyProjects.Domain.ReleaseAggregate;


namespace MyProjects.Projects.Api.Mapping
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Release, ProjectDto>();
            CreateMap<ProjectDto, Release>();
            CreateMap<CreateProjectDto, Release>();
            CreateMap<UpdateProjectDto, ProjectDto>();
        }
    }
}
