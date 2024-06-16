using Ddd.Dtos;
using MediatR;
using MyProjects.Application.Release.Dtos.Release;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetAllReleasesQuery : IRequest<IEnumerable<ReleaseDto>>
    {
        public GetAllReleasesQuery(PaginationDto pagination)
        {
            Pagination = pagination;
        }

        public PaginationDto Pagination { get; }
    }
}
