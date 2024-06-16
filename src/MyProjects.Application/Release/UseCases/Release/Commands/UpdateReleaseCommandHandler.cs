using MediatR;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class UpdateReleaseCommandHandler : IRequestHandler<UpdateReleaseCommand>
    {
        public Task Handle(UpdateReleaseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
