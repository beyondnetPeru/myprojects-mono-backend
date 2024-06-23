using AutoMapper;
using MediatR;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseFeaturesQueryHandler : IRequestHandler<GetReleaseFeaturesQuery, IEnumerable<ReleaseFeatureDto>>
    {
        private readonly IReleasesRepository repository;
        private readonly IMapper mapper;

        public GetReleaseFeaturesQueryHandler(IReleasesRepository repository, IMapper mapper) {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReleaseFeatureDto>> Handle(GetReleaseFeaturesQuery request, CancellationToken cancellationToken)
        {
            var features = await repository.GetFeatures(request.ReleaseId);

            var result = mapper.Map<IEnumerable<ReleaseFeatureDto>>(features);

            return result;
        }
    }
}
