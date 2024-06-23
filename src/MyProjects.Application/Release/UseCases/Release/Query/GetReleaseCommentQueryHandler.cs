using AutoMapper;
using MediatR;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseCommentQueryHandler : IRequestHandler<GetReleaseCommentQuery, ReleaseCommentDto>
    {
        private readonly IReleasesRepository repository;
        private readonly IMapper mapper;

        public GetReleaseCommentQueryHandler(IReleasesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ReleaseCommentDto> Handle(GetReleaseCommentQuery request, CancellationToken cancellationToken)
        {
            var data = await repository.GetComment(request.Id, request.CommentId);

            var result = mapper.Map<ReleaseCommentDto>(data);

            return result;
        }
    }
}
