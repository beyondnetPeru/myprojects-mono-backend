
namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseFeature
    {
        public string ProjectId { get; set; } = string.Empty;
        public string FeatureId { get; set; } = string.Empty;
        public string FeatureName { get; set; } = string.Empty;
        public string FeatureDescription { get; set; } = string.Empty;
        public List<ReleaseFeaturePhase> Phases { get; set; }
        public List<ReleaseFeatureComment> Comments { get; set; }
        public List<ReleaseFeatureRollout> Rollouts { get; set; }
        public string FeatureStatus { get; set; } = string.Empty;

        public ReleaseFeature(string projectId, string featureId, string featureName, string featureDescription)
        {
            ProjectId = projectId;
            FeatureId = featureId;
            FeatureName = featureName;
            FeatureDescription = featureDescription;
            Phases = new List<ReleaseFeaturePhase>();
            Comments = new List<ReleaseFeatureComment>();
            Rollouts = new List<ReleaseFeatureRollout>();
        }
    }
}
