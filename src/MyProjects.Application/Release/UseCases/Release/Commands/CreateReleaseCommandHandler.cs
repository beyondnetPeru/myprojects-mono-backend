using Ddd.ValueObjects;
using MediatR;
using MyProjects.Domain.ReleaseAggregate;
using MyProjects.Shared.Domain;


namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class CreateReleaseCommandHandler(IReleasesRepository repository) : IRequestHandler<CreateReleaseCommand>
    {
        public async Task Handle(CreateReleaseCommand request, CancellationToken cancellationToken)
        {
            var release = ReleaseEntity.Create(StringValueObject.Create(request.Title),
                                               StringValueObject.Create(request.Description));

            if (!release.IsValid)
                throw new DomainException(release.GetBrokenRules());

            await repository.Create(release);            
        }
    }
}
