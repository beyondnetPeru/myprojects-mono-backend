using MediatR;
using MyProjects.Application.Release.Dtos.Release;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseCommentQuery : IRequest<ReleaseCommentDto>
    {
        public GetReleaseCommentQuery(string id, string commentId)
        {
            Id = id;
            CommentId = commentId;
        }

        public string Id { get; }
        public string CommentId { get; }
    }
}
