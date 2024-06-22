namespace MyProjects.Infrastructure.Database.Tables
{
    public class FeaturePhaseTable
    {
        public string FeatureId { get; set; } = string.Empty;
        public required FeatureTable Feature { get; set; }
        public string PhaseId { get; set; } = string.Empty;
        public required PhaseTable Phase { get; set; }
    }
}
