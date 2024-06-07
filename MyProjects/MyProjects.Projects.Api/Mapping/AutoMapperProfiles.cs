using AutoMapper;
using MyProjects.Projects.Api.DTOs;
using MyProjects.Projects.Api.Models;

namespace MyProjects.Projects.Api.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, ProjectDto>();
        }
    }
}
