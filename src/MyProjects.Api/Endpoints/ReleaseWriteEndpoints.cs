
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MyProjects.Api.Common;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Application.Release.UseCases.Release.Commands;


namespace MyProjects.Projects.Api.Endpoints
{
    public static class ReleaseWriteEndpoints
    {
        public static RouteGroupBuilder MapProjects(this RouteGroupBuilder group)
        {
            group.MapPost("/", CreateAsync);
            group.MapPut("/{id}", UpdateAsync);
            group.MapDelete("/{id}", DeleteAsync);
            group.MapPut("/{id}/title", UpdateTitleAsync);
            group.MapPut("/{id}/description", UpdateDescriptionAsync);

            group.MapPost("/{id}/features", CreateReleaseFeatureAsync);

            return group;
        }


        public static async Task<Results<Ok, ValidationProblem>> CreateAsync(CreateReleaseDto createReleaseDto,
                                                                                              IMediator mediator,
                                                                                              IMapper mapper,
                                                                                              IOutputCacheStore outputCacheStore)
        {
            var command = mapper.Map<CreateReleaseDto, CreateReleaseCommand>(createReleaseDto);
            
            await mediator.Send(command);
            await ClearRefCache(outputCacheStore);

            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, BadRequest>> UpdateAsync(string id, ChangeReleaseTitleDto changeReleaseTitleDto,
                                                                                             IMediator mediator,
                                                                                             IOutputCacheStore outputCacheStore,
                                                                                             IMapper mapper)
        {
            var command = mapper.Map<ChangeReleaseTitleDto, ChangeReleaseTitleCommand>(changeReleaseTitleDto);
            
            await mediator.Send(command);
            await ClearRefCache(outputCacheStore);
            
            return TypedResults.Ok();
        }

        public static async Task<Results<NoContent, BadRequest>> DeleteAsync(string id, IMediator mediator, IOutputCacheStore outputCacheStore)
        {
            await mediator.Send(new DeleteReleaseCommand(id));

            await ClearRefCache(outputCacheStore);

            return TypedResults.NoContent();
        }

        public static async Task<Results<Ok, BadRequest>> UpdateTitleAsync(string id, ChangeReleaseTitleDto changeReleaseTitleDto, IMediator mediator,
                                                                                                                                                                                                                                                                                                IMapper mapper)
        {
            var command = mapper.Map<ChangeReleaseTitleDto, ChangeReleaseTitleCommand>(changeReleaseTitleDto);

            await mediator.Send(command);            

            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, BadRequest>> UpdateDescriptionAsync(string id, ChangeReleaseDescriptionDto changeReleaseDescriptionDto, IMediator mediator,
                                                                                                                                                                                                                                                                                                                                            IMapper mapper)
        {
            var command = mapper.Map<ChangeReleaseDescriptionDto, ChangeReleaseDescriptionCommand>(changeReleaseDescriptionDto);

            await mediator.Send(command);


            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, BadRequest>> CreateReleaseFeatureAsync(string id, CreateReleaseFeatureDto createReleaseFeatureDto, IMediator mediator,
                                                                                                                                                                                                                                                                                                           IMapper mapper)
        {
            var command = mapper.Map<CreateReleaseFeatureDto, CreateReleaseFeatureCommand>(createReleaseFeatureDto);

            await mediator.Send(command);

            return TypedResults.Ok();
        }


        static async Task ClearRefCache(IOutputCacheStore outputCacheStore)
        {
            await outputCacheStore.EvictByTagAsync(Tags.TAG_CACHE_RELEASE, default);
        }
    }
}
