using MediatR;
using MyProjects.Application.Release.Dtos.Release;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseFeatureQuery : IRequest<ReleaseFeatureDto>
    {
        public GetReleaseFeatureQuery(string id, string featureId)
        {
            Id = id;
            FeatureId = featureId;
        }

        public string Id { get; }
        public string FeatureId { get; }
    }
}
