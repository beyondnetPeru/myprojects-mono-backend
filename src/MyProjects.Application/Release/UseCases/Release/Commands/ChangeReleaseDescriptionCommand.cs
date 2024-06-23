using MediatR;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class ChangeReleaseDescriptionCommand : IRequest
    {
        public string Id { get; }
        public string Description { get; }

        public ChangeReleaseDescriptionCommand(string id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
