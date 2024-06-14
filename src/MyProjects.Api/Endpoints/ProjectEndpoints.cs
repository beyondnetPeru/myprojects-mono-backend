
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MyProjects.Application.Dtos.Project;
using MyProjects.Application.Facade;


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

        public static async Task<Ok<IEnumerable<ProjectDto>>> GetAllAsync(IProjectApplication projectApplication)
        {
            return TypedResults.Ok(await projectApplication.GetAll());
        }

        public static async Task<Results<Ok<ProjectDto>, NotFound>> GetByIdAsync(string id, IProjectApplication projectApplication)
        {
            var result = await projectApplication.GetById(id);

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result);
        }

        public static async Task<Results<Created<ProjectDto>, ValidationProblem>> CreateAsync(CreateProjectDto createProjectDto,
                                                                                              IProjectApplication projectApplication, 
                                                                                              IMapper mapper,
                                                                                              IOutputCacheStore outputCacheStore)
        {
            var projectDto = mapper.Map<ProjectDto>(createProjectDto);

            await projectApplication.Create(projectDto);

            await ClearRefCache(outputCacheStore);

            return TypedResults.Created($"/{projectDto.Id}", projectDto);
        }

        public static async Task<Results<Ok<ProjectDto>, BadRequest>> UpdateAsync(string id, UpdateProjectDto updateProjectDto, 
                                                                                             IProjectApplication projectApplication, 
                                                                                             IOutputCacheStore outputCacheStore, 
                                                                                             IMapper mapper)
        {
            var projectDto = mapper.Map<ProjectDto>(updateProjectDto);

            var result = await projectApplication.Update(id, projectDto);

            if (result == null)
            {
                return TypedResults.BadRequest();
            }

            await ClearRefCache(outputCacheStore);

            return TypedResults.Ok(result);
        }

        public static async Task<Results<NoContent, BadRequest>> DeleteAsync(string id, IProjectApplication projectApplication, IOutputCacheStore outputCacheStore)
        {
            await projectApplication.Delete(id);

            await ClearRefCache(outputCacheStore);

            return TypedResults.NoContent();

        }



        static async Task ClearRefCache(IOutputCacheStore outputCacheStore)
        {
            await outputCacheStore.EvictByTagAsync("projects-get", default);
        }
    }
}
