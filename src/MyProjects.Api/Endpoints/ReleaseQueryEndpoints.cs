using Ddd;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MyProjects.Api.Common;
using MyProjects.Application.Project.Dtos;
using MyProjects.Application.Project.UseCases.Project.Query;
using MyProjects.Shared.Application.Cache;


namespace MyProjects.Projects.Api.Endpoints
{
    public static class ProjectQueryEndpoints
    {
        public static RouteGroupBuilder MapProjects(this RouteGroupBuilder group, IConfiguration configuration)
        {
            group.MapGet("/", GetAllAsync)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(
                    MemoryTree.TimeToLive(configuration)
                    )).Tag(Tags.TAG_CACHE_Project));

            group.MapGet("/{id}", GetByIdAsync);

            group.MapGet("/{id}/features", GetProjectFeaturesAsync);

            group.MapGet("/{id}/features/{featureId}", GetProjectFeatureByIdAsync);

            group.MapGet("/{id}/references", GetProjectReferencesAsync);

            group.MapGet("/{id}references/{referenceId}", GetProjectReferenceByIdAsync);

            group.MapGet("/{id}/comments", GetProjectCommentsAsync);

            group.MapGet("/{id}comments/{commentId}", GetProjectCommentByIdAsync);

            return group;
        }

        public static async Task<Ok<IEnumerable<ProjectDto>>> GetAllAsync(IMediator mediator, int page = 1, int recordsPerPage = 10)
        {
            var pagination = new Pagination(page);
            
            pagination.RecordsPerPage = recordsPerPage;

            var result = await mediator.Send(new GetAllProjectsQuery(pagination));

            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<ProjectDto>, NotFound>> GetByIdAsync(string id, IMediator mediator)
        {
            var result = await mediator.Send(new GetProjectQuery(id));

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result);
        }

        public static async Task<Ok<IEnumerable<ProjectFeatureDto>>> GetProjectFeaturesAsync(string id, IMediator mediator)
        {
            var result = await mediator.Send(new GetProjectFeaturesQuery(id));

            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<ProjectFeatureDto>, NotFound>> GetProjectFeatureByIdAsync(string id, string featureId, IMediator mediator)
        {
            var result = await mediator.Send(new GetProjectFeatureQuery(id, featureId));

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result);
        }

        public static async Task<Ok<IEnumerable<ProjectReferenceDto>>> GetProjectReferencesAsync(string id, IMediator mediator)
        {
            var result = await mediator.Send(new GetProjectReferencesQuery(id));

            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<ProjectReferenceDto>, NotFound>> GetProjectReferenceByIdAsync(string id, string referenceId, IMediator mediator)
        {
            var result = await mediator.Send(new GetProjectReferenceQuery(id, referenceId));

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result);
        }

        public static async Task<Ok<IEnumerable<ProjectCommentDto>>> GetProjectCommentsAsync(string id, IMediator mediator)
        {
            var result = await mediator.Send(new GetProjectCommentsQuery(id));

            return TypedResults.Ok(result);
        }

        public static async Task<Results<Ok<ProjectCommentDto>, NotFound>> GetProjectCommentByIdAsync(string id, string commentId, IMediator mediator)
        {
            var result = await mediator.Send(new GetProjectCommentQuery(id, commentId));

            if (result == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result);
        }
    }
}
