using Ddd.ValueObjects;
using MediatR;
using MyProjects.Domain.ReleaseAggregate;
using MyProjects.Shared.Domain;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class ScheduleReleaseCommandHandler(IReleasesRepository repository) : IRequestHandler<ScheduleReleaseCommand>
    {
        public async Task Handle(ScheduleReleaseCommand request, CancellationToken cancellationToken)
        {
            var release = await repository.GetById(request.ReleaseId);

            release.Schedule(DateTimeValueObject.Create(request.GoLiveDate));

            if (!release.IsValid)
                throw new DomainException(release.GetBrokenRules());

            await repository.Update(release);
        }
    }
}
