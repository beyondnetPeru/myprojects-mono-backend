using AutoMapper;
using MediatR;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseQueryHandler : IRequestHandler<GetReleaseQuery, ReleaseDto>
    {
        private readonly IReleasesRepository repository;
        private readonly IMapper mapper;

        public GetReleaseQueryHandler(IReleasesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ReleaseDto> Handle(GetReleaseQuery request, CancellationToken cancellationToken)
        {
            var data = await repository.GetById(request.Id);

            var dto = mapper.Map<ReleaseDto>(data);

            return dto;
        }
    }
}
