
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MyProjects.Api.Common;
using MyProjects.Application.Project.Dtos;
using MyProjects.Application.Project.UseCases.Project.Commands;


namespace MyProjects.Projects.Api.Endpoints
{
    public static class ProjectWriteEndpoints
    {
        public static RouteGroupBuilder MapProjects(this RouteGroupBuilder group)
        {
            group.MapPost("/", CreateAsync);
            group.MapPut("/{id}", UpdateAsync);
            group.MapDelete("/{id}", DeleteAsync);
            group.MapPut("/{id}/title", UpdateTitleAsync);
            group.MapPut("/{id}/description", UpdateDescriptionAsync);

            group.MapPost("/{id}/features", CreateProjectFeatureAsync);

            return group;
        }


        public static async Task<Results<Ok, ValidationProblem>> CreateAsync(CreateProjectDto createProjectDto,
                                                                                              IMediator mediator,
                                                                                              IMapper mapper,
                                                                                              IOutputCacheStore outputCacheStore)
        {
            var command = mapper.Map<CreateProjectDto, CreateProjectCommand>(createProjectDto);
            
            await mediator.Send(command);
            await ClearRefCache(outputCacheStore);

            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, BadRequest>> UpdateAsync(string id, ChangeProjectTitleDto changeProjectTitleDto,
                                                                                             IMediator mediator,
                                                                                             IOutputCacheStore outputCacheStore,
                                                                                             IMapper mapper)
        {
            var command = mapper.Map<ChangeProjectTitleDto, ChangeProjectTitleCommand>(changeProjectTitleDto);
            
            await mediator.Send(command);
            await ClearRefCache(outputCacheStore);
            
            return TypedResults.Ok();
        }

        public static async Task<Results<NoContent, BadRequest>> DeleteAsync(string id, IMediator mediator, IOutputCacheStore outputCacheStore)
        {
            await mediator.Send(new DeleteProjectCommand(id));

            await ClearRefCache(outputCacheStore);

            return TypedResults.NoContent();
        }

        public static async Task<Results<Ok, BadRequest>> UpdateTitleAsync(string id, ChangeProjectTitleDto changeProjectTitleDto, IMediator mediator,
                                                                                                                                                                                                                                                                                                IMapper mapper)
        {
            var command = mapper.Map<ChangeProjectTitleDto, ChangeProjectTitleCommand>(changeProjectTitleDto);

            await mediator.Send(command);            

            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, BadRequest>> UpdateDescriptionAsync(string id, ChangeProjectDescriptionDto changeProjectDescriptionDto, IMediator mediator,
                                                                                                                                                                                                                                                                                                                                            IMapper mapper)
        {
            var command = mapper.Map<ChangeProjectDescriptionDto, ChangeProjectDescriptionCommand>(changeProjectDescriptionDto);

            await mediator.Send(command);


            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, BadRequest>> CreateProjectFeatureAsync(string id, CreateProjectFeatureDto createProjectFeatureDto, IMediator mediator,
                                                                                                                                                                                                                                                                                                           IMapper mapper)
        {
            var command = mapper.Map<CreateProjectFeatureDto, CreateProjectFeatureCommand>(createProjectFeatureDto);

            await mediator.Send(command);

            return TypedResults.Ok();
        }


        static async Task ClearRefCache(IOutputCacheStore outputCacheStore)
        {
            await outputCacheStore.EvictByTagAsync(Tags.TAG_CACHE_Project, default);
        }
    }
}
