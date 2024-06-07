using AutoMapper;
using MyProjects.Application.Dtos.Project;
using MyProjects.Domain.ProjectAggregate;


namespace MyProjects.Projects.Api.Mapping
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, ProjectDto>();
        }
    }
}
