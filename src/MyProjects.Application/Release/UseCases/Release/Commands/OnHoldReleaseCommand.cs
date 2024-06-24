using MediatR;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class OnHoldReleaseCommand : IRequest
    {
        public string ReleaseId { get; set; }

        public OnHoldReleaseCommand(string releaseId)
        {
            ReleaseId = releaseId;
        }
    }
}
