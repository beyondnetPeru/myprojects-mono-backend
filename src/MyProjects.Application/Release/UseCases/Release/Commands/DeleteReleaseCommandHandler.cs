using MediatR;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class DeleteReleaseCommandHandler(IReleasesRepository repository) : IRequestHandler<DeleteReleaseCommand>
    {
        public async Task Handle(DeleteReleaseCommand request, CancellationToken cancellationToken)
        {
            await repository.Delete(request.Id);
        }
    }
}
