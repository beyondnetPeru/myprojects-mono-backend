using MediatR;
using MyProjects.Domain.ReleaseAggregate;
using MyProjects.Shared.Domain;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class OnHoldReleaseCommandHandler(IReleasesRepository repository) : IRequestHandler<OnHoldReleaseCommand>
    {
        public async Task Handle(OnHoldReleaseCommand request, CancellationToken cancellationToken)
        {
            var release = await repository.GetById(request.ReleaseId);

            release.OnHold();

            if (!release.IsValid)
                throw new DomainException(release.GetBrokenRules());

            await repository.Update(release);
        }
    }
}
