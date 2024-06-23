using MediatR;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class OpenReleaseCommandHandler(IReleasesRepository repository) : IRequestHandler<OpenReleaseCommand>
    {
        public async Task Handle(OpenReleaseCommand request, CancellationToken cancellationToken)
        {
            var release = await repository.GetById(request.ReleaseId);

            release.Open();

            await repository.Update(release);
        }
    }
}
