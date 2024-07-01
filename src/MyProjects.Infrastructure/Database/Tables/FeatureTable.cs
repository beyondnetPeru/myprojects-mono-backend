namespace MyProjects.Infrastructure.Database.Tables
{
    public class FeatureTable
    {
        public string Id { get; set; } = string.Empty;

        public string ProjectId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<FeaturePhaseTable>? Phases{ get; set; }
        public ICollection<FeatureCommentTable>? Comments { get; set; }
        public ICollection<FeatureRolloutTable>? Rollouts { get; set; }
        public int Status { get; set; } = 1;
    }
}
