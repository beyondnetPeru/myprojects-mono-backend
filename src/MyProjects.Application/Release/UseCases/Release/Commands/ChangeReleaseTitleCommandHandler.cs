using Ddd.ValueObjects;
using MediatR;
using MyProjects.Domain.ReleaseAggregate;
using MyProjects.Shared.Domain;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class ChangeReleaseTitleCommandHandler(
        IReleasesRepository repository
        ) : IRequestHandler<ChangeReleaseTitleCommand>
    {
        public async Task Handle(ChangeReleaseTitleCommand request, CancellationToken cancellationToken)
        {

            var release = await repository.GetById(request.Id);

            release.ChangeTitle(StringValueObject.Create(request.Title));

            if (!release.IsValid)
                throw new DomainException(release.GetBrokenRules());

            await repository.Update(release);
        }
    }
}
