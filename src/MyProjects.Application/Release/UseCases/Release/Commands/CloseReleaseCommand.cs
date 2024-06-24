using MediatR;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class CloseReleaseCommand : IRequest
    {
        public string ReleaseId { get; set; }

        public CloseReleaseCommand(string releaseId)
        {
            ReleaseId = releaseId;
        }
    }
}
