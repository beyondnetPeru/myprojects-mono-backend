using MediatR;
using MyProjects.Application.Release.Dtos.Release;


namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseCommentsQuery: IRequest<IEnumerable<ReleaseCommentDto>>
    {
        public GetReleaseCommentsQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
