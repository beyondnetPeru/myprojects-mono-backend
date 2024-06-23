using AutoMapper;
using MediatR;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Release.UseCases.Release.Query
{
    public class GetReleaseCommentsQueryHandler : IRequestHandler<GetReleaseCommentsQuery, IEnumerable<ReleaseCommentDto>>
    {
        private readonly IReleasesRepository repository;
        private readonly IMapper mapper;

        public GetReleaseCommentsQueryHandler(IReleasesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReleaseCommentDto>> Handle(GetReleaseCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await repository.GetComments(request.Id);

            var result = mapper.Map<IEnumerable<ReleaseCommentDto>>(comments);

            return result;
        }
    }
}
