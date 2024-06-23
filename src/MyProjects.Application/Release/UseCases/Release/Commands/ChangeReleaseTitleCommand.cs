using MediatR;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class ChangeReleaseTitleCommand: IRequest
    {
        public string Id { get; }
        public string Title { get; }

        public ChangeReleaseTitleCommand(string id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
