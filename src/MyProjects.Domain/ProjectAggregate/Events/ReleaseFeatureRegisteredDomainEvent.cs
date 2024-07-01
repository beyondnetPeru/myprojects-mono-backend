using Ddd;

namespace MyProjects.Domain.ProjectAggregate.Events
{
    public class ProjectFeatureRegisteredDomainEvent : DomainEvent
    {
        public string FeatureId { get; }
        public string ProjectId { get; }
        public string FeatureName { get; }

        public ProjectFeatureRegisteredDomainEvent(string projectId, string featureId, string featureName)
        {
            ProjectId = projectId;
            FeatureId = featureId;
            FeatureName = featureName;
        }
    }
}
