using MediatR;
using MyProjects.Application.Release.Dtos.Release;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseReferenceQuery: IRequest<ReleaseReferenceDto>
    {
        public string Id { get; set; }
        public string ReferenceId { get; }

        public GetReleaseReferenceQuery(string id, string referenceId)
        {
            Id = id;
            ReferenceId = referenceId;
        }
    }
}
