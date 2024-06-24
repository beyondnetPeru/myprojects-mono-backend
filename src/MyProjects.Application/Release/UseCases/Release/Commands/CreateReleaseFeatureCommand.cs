using MediatR;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class CreateReleaseFeatureCommand : IRequest
    {
        public string ReleaseId { get; private set; } 
        public string FeatureName { get; private set; } 
        public string FeatureDescription { get; private set; } 

        public CreateReleaseFeatureCommand(string releaseId, string featureId, string featureName, string featureDescription = "")
        {
            ReleaseId = releaseId;
            FeatureName = featureName;
            FeatureDescription = featureDescription;
        }
    }
}
