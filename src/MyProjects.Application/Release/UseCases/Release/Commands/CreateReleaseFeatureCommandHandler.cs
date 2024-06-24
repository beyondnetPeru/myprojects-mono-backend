using AutoMapper;
using Ddd.ValueObjects;
using MediatR;
using MyProjects.Domain.ReleaseAggregate;
using MyProjects.Shared.Domain;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class CreateReleaseFeatureCommandHandler(IReleasesRepository repository) : IRequestHandler<CreateReleaseFeatureCommand>
    {
        public async Task Handle(CreateReleaseFeatureCommand request, CancellationToken cancellationToken)
        {
            var release = await repository.GetById(request.ReleaseId);

            var releaseFeature = ReleaseFeature.Create(IdValueObject.SetValue(request.ReleaseId), StringValueObject.Create(request.FeatureName), StringValueObject.Create(request.FeatureDescription));

            release.AddFeature(releaseFeature);

            if (!release.IsValid)
                throw new DomainException(release.GetBrokenRules());

            await repository.AddFeature(releaseFeature);
        }
    }
}
