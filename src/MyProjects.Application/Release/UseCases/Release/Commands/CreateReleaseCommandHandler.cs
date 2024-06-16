using MediatR;


namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class CreateReleaseCommandHandler : IRequestHandler<CreateReleaseCommand>
    {
        public Task Handle(CreateReleaseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
