
using Ddd.ValueObjects;
using MediatR;
using MyProjects.Domain.ReleaseAggregate;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class ChangeReleaseDescriptionCommandHandler(
        IReleasesRepository repository
        ) : IRequestHandler<ChangeReleaseDescriptionCommand>
    {
        public async Task Handle(ChangeReleaseDescriptionCommand request, CancellationToken cancellationToken)
        {
            var release = await repository.GetById(request.Id);
            
            release.ChangeDescription(StringValueObject.Create(request.Description));

            await repository.Update(release);
        }
    }
}
