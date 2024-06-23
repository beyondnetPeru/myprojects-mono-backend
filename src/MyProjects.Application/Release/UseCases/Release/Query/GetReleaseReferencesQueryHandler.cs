using AutoMapper;
using MediatR;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseReferencesQueryHandler : IRequestHandler<GetReleaseReferencesQuery, IEnumerable<ReleaseReferenceDto>>
    {
        private readonly IReleasesRepository repository;
        private readonly IMapper mapper;

        public GetReleaseReferencesQueryHandler(IReleasesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReleaseReferenceDto>> Handle(GetReleaseReferencesQuery request, CancellationToken cancellationToken)
        {
            var release = await repository.GetReferences(request.Id);

            return mapper.Map<IEnumerable<ReleaseReferenceDto>>(release);
        }
    }
}
