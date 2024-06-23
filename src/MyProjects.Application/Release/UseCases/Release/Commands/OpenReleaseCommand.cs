using MediatR;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public  class OpenReleaseCommand : IRequest
    {
        public string ReleaseId { get; set; }

        public OpenReleaseCommand(string releaseId)
        {
            ReleaseId = releaseId;
        }
    }
}
