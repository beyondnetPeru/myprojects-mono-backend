using AutoMapper;
using Ddd.ValueObjects;
using MediatR;
using MyProjects.Domain.ReleaseAggregate;


namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class CreateReleaseCommandHandler : IRequestHandler<CreateReleaseCommand>
    {
        private readonly IReleasesRepository repository;
        private readonly IMapper mapper;

        public CreateReleaseCommandHandler(IReleasesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task Handle(CreateReleaseCommand request, CancellationToken cancellationToken)
        {
            var release = ReleaseEntity.Create(StringValueObject.Create(request.Title),
                                               StringValueObject.Create(request.Description));

            release.Validate(release);

            if (!release.IsValid)
            {
                throw new Exception(release.GetBrokenRules().ToString());
            }

            repository.Create(release);

            return Task.CompletedTask;
        }
    }
}
