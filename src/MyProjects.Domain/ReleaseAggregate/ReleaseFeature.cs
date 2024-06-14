
using Ddd;
using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseFeature :  Entity<ReleaseFeature>
    {
        public IdValueObject ProjectId { get; set; }        
        public StringValueObject FeatureName { get; set; }
        public StringValueObject? FeatureDescription { get; set; }
        public List<ReleaseFeaturePhase> Phases { get; set; }
        public List<ReleaseFeatureComment> Comments { get; set; }
        public List<ReleaseFeatureRollout> Rollouts { get; set; }
        public ReleaseFeatureStatus FeatureStatus { get; set; }

        public ReleaseFeature(IdValueObject projectId, StringValueObject featureName)
        {
            ProjectId = projectId;            
            FeatureName = featureName;
            FeatureDescription = StringValueObject.Create(string.Empty);

            Phases = new List<ReleaseFeaturePhase>();
            Comments = new List<ReleaseFeatureComment>();
            Rollouts = new List<ReleaseFeatureRollout>();
        }
    }
}
