
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MyProjects.Projects.Api.DTOs;
using MyProjects.Projects.Api.Models;
using MyProjects.Projects.Api.Repositories;

namespace MyProjects.Projects.Api.Endpoints
{
    public static class ProjectEndpoints
    {

        public static RouteGroupBuilder MapProjects(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAllAsync)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("projects-get"));

            group.MapGet("/{id}", GetByIdAsync);

            group.MapPost("/", CreateAsync);

            group.MapPut("/{id}", UpdateAsync);

            group.MapDelete("/{id}", DeleteAsync);

            return group;
        }

        public static async Task<Ok<List<ProjectDto>>> GetAllAsync(IProjectsRepository repository, IMapper mapper)
        {
            var projects = await repository.GetAll();

            var dtos = mapper.Map<List<ProjectDto>>(projects);

            return TypedResults.Ok(dtos);
        }

        public static async Task<Results<Ok<ProjectDto>, NotFound>> GetByIdAsync(string id, IProjectsRepository repository, IMapper mapper)
        {
            var project = await repository.GetById(id);

            if (project == null)
            {
                return TypedResults.NotFound();
            }


            var projectDto = mapper.Map<ProjectDto>(project);   

            return TypedResults.Ok(projectDto);
        }

        public static async Task<Results<Created<ProjectDto>, BadRequest>> CreateAsync(CreateProjectDto projectDto, IProjectsRepository repository, IOutputCacheStore outputCacheStore, IMapper mapper)
        {

            var project = mapper.Map<Project>(projectDto);

            var id = await repository.Create(project);

            var dto = mapper.Map<ProjectDto>(project);

            await ClearRefCache(outputCacheStore);

            return TypedResults.Created($"/{id}", dto);
        }

        public static async Task<Results<Ok<ProjectDto>, BadRequest>> UpdateAsync(string id, UpdateProjectDto projectDto, IProjectsRepository repository, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var exists = await repository.Exists(id);

            if (!exists)
            {
                return TypedResults.BadRequest();
            }

            var project = mapper.Map<Project>(projectDto);


            await repository.Update(project);

            var dto = mapper.Map<ProjectDto>(project);

            await ClearRefCache(outputCacheStore);

            return TypedResults.Ok(dto);
        }

        public static async Task<Results<NoContent, BadRequest>> DeleteAsync(string id, IProjectsRepository repository, IOutputCacheStore outputCacheStore)
        {
            var exists = await repository.Exists(id);

            if (!exists)
            {
                return TypedResults.BadRequest();
            }

            await repository.Delete(id);

            await ClearRefCache(outputCacheStore);

            return TypedResults.NoContent();
        }

        static async Task ClearRefCache(IOutputCacheStore outputCacheStore)
        {
            await outputCacheStore.EvictByTagAsync("projects-get", default);
        }
    }
}
