using MediatR;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class DeleteReleaseCommandHandler : IRequestHandler<DeleteReleaseCommand>
    {
        public Task Handle(DeleteReleaseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
