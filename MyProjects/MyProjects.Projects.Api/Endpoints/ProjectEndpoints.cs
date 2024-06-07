
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MyProjects.Projects.Api.Models;
using MyProjects.Projects.Api.Repositories;

namespace MyProjects.Projects.Api.Endpoints
{
    public static class ProjectEndpoints
    {

        public static RouteGroupBuilder MapProjects(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetProjectsAsync)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("projects-get"));

            group.MapGet("/{id}", GetProjectByIdAsync);

            group.MapPost("/", CreateProjectAsync);

            group.MapPut("/{id}", UpdateProjectAsync);

            group.MapDelete("/{id}", DeleteProjectAsync);

            return group;
        }

        public static async Task<Ok<List<Project>>> GetProjectsAsync(IProjectsRepository repository)
        {
            var projects = await repository.GetAll();

            return TypedResults.Ok(projects.ToList());
        }

        public static async Task<Results<Ok<Project>, NotFound>> GetProjectByIdAsync(string id, IProjectsRepository repository)
        {
            var project = await repository.GetById(id);

            if (project == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(project);
        }

        public static async Task<Results<Created<Project>, BadRequest>> CreateProjectAsync(Project project, IProjectsRepository repository, IOutputCacheStore outputCacheStore)
        {
            var id = await repository.Create(project);

            await ClearRefCache(outputCacheStore);

            return TypedResults.Created($"/{id}", project);
        }

        public static async Task<Results<Ok<Project>, BadRequest>> UpdateProjectAsync(string id, Project project, IProjectsRepository repository, IOutputCacheStore outputCacheStore)
        {
            var exists = await repository.Exists(id);

            if (!exists)
            {
                return TypedResults.BadRequest();
            }

            await repository.Update(project);

            await ClearRefCache(outputCacheStore);

            return TypedResults.Ok(project);
        }

        public static async Task<Results<NoContent, BadRequest>> DeleteProjectAsync(string id, IProjectsRepository repository, IOutputCacheStore outputCacheStore)
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
