using AutoMapper;
using MediatR;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseReferenceQueryHandler : IRequestHandler<GetReleaseReferenceQuery, ReleaseReferenceDto>
    {
        private readonly IReleasesRepository repository;
        private readonly IMapper mapper;

        public GetReleaseReferenceQueryHandler(IReleasesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ReleaseReferenceDto> Handle(GetReleaseReferenceQuery request, CancellationToken cancellationToken)
        {
            var release = await repository.GetReference(request.Id, request.ReferenceId);

            return mapper.Map<ReleaseReferenceDto>(release);
        }
    }
}
