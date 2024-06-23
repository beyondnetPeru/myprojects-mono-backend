using AutoMapper;
using MediatR;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseFeatureQueryHandler : IRequestHandler<GetReleaseFeatureQuery, ReleaseFeatureDto>
    {
        private readonly IReleasesRepository repository;
        private readonly IMapper mapper;

        public GetReleaseFeatureQueryHandler(IReleasesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ReleaseFeatureDto> Handle(GetReleaseFeatureQuery request, CancellationToken cancellationToken)
        {
            var feature = await repository.GetFeature(request.Id, request.FeatureId);

            var result = mapper.Map<ReleaseFeatureDto>(feature);

            return result;
        }
    }
}
