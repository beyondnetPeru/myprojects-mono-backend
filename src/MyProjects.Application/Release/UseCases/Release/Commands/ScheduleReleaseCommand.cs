using MediatR;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class ScheduleReleaseCommand : IRequest
    {
        public string ReleaseId { get; set; }
        public DateTime GoLiveDate { get; set; }

        public ScheduleReleaseCommand(string releaseId, DateTime goLiveDate)
        {
            ReleaseId = releaseId;
            GoLiveDate = goLiveDate;
        }
    }
}
