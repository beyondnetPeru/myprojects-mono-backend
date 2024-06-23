
using Ddd.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MyProjects.Api.Common;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Application.Release.UseCases.Release.Query;
using MyProjects.Shared.Application.Cache;


namespace MyProjects.Projects.Api.Endpoints
{
    public static class ReleaseQueryEndpoints
    {
        public static RouteGroupBuilder MapProjects(this RouteGroupBuilder group, IConfiguration configuration)
        {
            group.MapGet("/", GetAllAsync)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(
                    MemoryTree.TimeToLive(configuration)
                    )).Tag(Tags.TAG_CACHE_RELEASE));

            group.MapGet("/{id}", GetByIdAsync);

            group.MapGet("/{id}/features", GetReleaseFeaturesAsync);

            group.MapGet("/{id}/features/{featureId}", GetReleaseFeatureByIdAsync);

            group.MapGet("/{id}/references", GetReleaseReferencesAsync);

            group.MapGet("/{id}references/{referenceId}", GetReleaseReferenceByIdAsync);

            group.MapGet("/{id}/comments", GetReleaseCommentsAsync);

            group.MapGet("/{id}comments/{commentId}", GetReleaseCommentByIdAsync);

            return group;
        }

        public static async Task<Ok<IEnumerable<ReleaseDto>>> GetAllAsync(IMediator mediator, int page = 1, int recordsPerPage = 10)
        {
            var pagination = new PaginationDto(page);
            
            pagination.RecordsPerPage = recordsPerPage;

            var result = await mediator.Send(new GetAllReleasesQuery(pagination));

            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<ReleaseDto>, NotFound>> GetByIdAsync(string id, IMediator mediator)
        {
            var result = await mediator.Send(new GetReleaseQuery(id));

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result);
        }

        public static async Task<Ok<IEnumerable<ReleaseFeatureDto>>> GetReleaseFeaturesAsync(string id, IMediator mediator)
        {
            var result = await mediator.Send(new GetReleaseFeaturesQuery(id));

            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<ReleaseFeatureDto>, NotFound>> GetReleaseFeatureByIdAsync(string id, string featureId, IMediator mediator)
        {
            var result = await mediator.Send(new GetReleaseFeatureQuery(id, featureId));

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result);
        }

        public static async Task<Ok<IEnumerable<ReleaseReferenceDto>>> GetReleaseReferencesAsync(string id, IMediator mediator)
        {
            var result = await mediator.Send(new GetReleaseReferencesQuery(id));

            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<ReleaseReferenceDto>, NotFound>> GetReleaseReferenceByIdAsync(string id, string referenceId, IMediator mediator)
        {
            var result = await mediator.Send(new GetReleaseReferenceQuery(id, referenceId));

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result);
        }

        public static async Task<Ok<IEnumerable<ReleaseCommentDto>>> GetReleaseCommentsAsync(string id, IMediator mediator)
        {
            var result = await mediator.Send(new GetReleaseCommentsQuery(id));

            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<ReleaseCommentDto>, NotFound>> GetReleaseCommentByIdAsync(string id, string commentId, IMediator mediator)
        {
            var result = await mediator.Send(new GetReleaseCommentQuery(id, commentId));

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result);
        }
    }
}
