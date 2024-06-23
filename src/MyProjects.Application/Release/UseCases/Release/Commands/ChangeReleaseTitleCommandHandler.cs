using Ddd.ValueObjects;
using MediatR;
using MyProjects.Domain.ReleaseAggregate;

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

            await repository.Update(release);
        }
    }
}
