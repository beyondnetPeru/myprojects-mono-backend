using MediatR;
using MyProjects.Application.Release.Dtos.Release;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseFeaturesQuery : IRequest<IEnumerable<ReleaseFeatureDto>>
    {
        public GetReleaseFeaturesQuery(string releaseId)
        {
            ReleaseId = releaseId;
        }

        public string ReleaseId { get; }
    }
}
