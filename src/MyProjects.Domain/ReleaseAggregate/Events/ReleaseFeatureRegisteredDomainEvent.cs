using Ddd;

namespace MyProjects.Domain.ReleaseAggregate.Events
{
    public class ReleaseFeatureRegisteredDomainEvent : DomainEvent
    {
        public string FeatureId { get; }
        public string ProjectId { get; }
        public string FeatureName { get; }

        public ReleaseFeatureRegisteredDomainEvent(string projectId, string featureId, string featureName)
        {
            ProjectId = projectId;
            FeatureId = featureId;
            FeatureName = featureName;
        }
    }
}
