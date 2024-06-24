using MediatR;
using MyProjects.Domain.ReleaseAggregate;
using MyProjects.Shared.Domain;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class CloseReleaseCommandHandler(IReleasesRepository repository) : IRequestHandler<CloseReleaseCommand>
    {
        public async Task Handle(CloseReleaseCommand request, CancellationToken cancellationToken)
        {
            var release = await repository.GetById(request.ReleaseId);

            release.Close();

            if (!release.IsValid)
                throw new DomainException(release.GetBrokenRules());

            await repository.Update(release);
        }
    }
}
