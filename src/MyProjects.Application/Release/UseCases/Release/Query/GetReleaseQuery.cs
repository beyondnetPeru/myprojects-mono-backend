using MediatR;
using MyProjects.Application.Release.Dtos.Release;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseQuery : IRequest<ReleaseDto>
    {
        public string Id { get; private set; }

        public GetReleaseQuery(string id)
        {
            Id = id;
        }
    }
}
