
namespace MyProjects.Infrastructure.Database.Tables
{
    public class FeatureRolloutTable
    {
        public string FeatureId { get; set; } = string.Empty;
        public required FeatureTable Feature { get; set; }
        public string RolloutId { get; set; } = string.Empty;
        public required RolloutTable Rollout { get; set; }
    }
}
