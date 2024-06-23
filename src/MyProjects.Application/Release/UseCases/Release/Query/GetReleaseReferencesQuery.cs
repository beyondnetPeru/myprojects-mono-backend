using MediatR;
using MyProjects.Application.Release.Dtos.Release;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseReferencesQuery :  IRequest<IEnumerable<ReleaseReferenceDto>>
    {
        public GetReleaseReferencesQuery(string id)
        {
            Id = id;
        }

        public string Id{ get; }
    }
}
