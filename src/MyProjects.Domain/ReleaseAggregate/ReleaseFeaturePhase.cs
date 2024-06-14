
namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseFeaturePhase
    {
        public ReleaseFeaturePhaseName Name { get; set; } 
        public ReleaseFeatureStartDate StartDate { get; set; }
        public ReleaseFeatureEndDate EndDate { get; set; }
        public int Status { get; set; } = 1;
    }
}
