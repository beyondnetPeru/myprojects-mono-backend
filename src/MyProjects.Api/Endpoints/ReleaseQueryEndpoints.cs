﻿
using Ddd.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Application.Release.UseCases.Release.Query;


namespace MyProjects.Projects.Api.Endpoints
{
    public static class ReleaseQueryEndpoints
    {
        public static RouteGroupBuilder MapProjects(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAllAsync)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("projects-get"));
            group.MapGet("/{id}", GetByIdAsync);
            
            return group;
        }

        public static async Task<Ok<IEnumerable<ReleaseDto>>> GetAllAsync(IMediator mediator, int page = 1, int recordsPerPage = 10)
        {
            var pagination = new PaginationDto()
            {
                Page = page,
                RecordsPerPage = recordsPerPage
            };

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

    }
}
