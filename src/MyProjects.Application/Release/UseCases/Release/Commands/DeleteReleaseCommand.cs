using MediatR;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class DeleteReleaseCommand: IRequest
    {
        public string Id { get; }

        public DeleteReleaseCommand(string id)
        {
            Id = id;
        }
    }
}
