using AutoMapper;
using Ddd.Dtos;
using MediatR;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetAllReleasesQueryHandler : IRequestHandler<GetAllReleasesQuery, IEnumerable<ReleaseDto>>
    {
        private readonly IReleasesRepository repository;
        private readonly IMapper mapper;

        public GetAllReleasesQueryHandler(IReleasesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReleaseDto>> Handle(GetAllReleasesQuery request, CancellationToken cancellationToken)
        {
            var data = await repository.GetAll(request.Pagination);

            var result = mapper.Map<IEnumerable<ReleaseDto>>(data);

            return result;
        }
    }
}
