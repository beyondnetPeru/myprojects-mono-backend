
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
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



        static async Task ClearRefCache(IOutputCacheStore outputCacheStore)
        {
            await outputCacheStore.EvictByTagAsync("projects-get", default);
        }
    }
}
