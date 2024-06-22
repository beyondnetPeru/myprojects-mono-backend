namespace MyProjects.Infrastructure.Database.Tables
{
    public class ReleaseFeatureTable
    {
        public string ReleaseId { get; set; } = string.Empty;
        public required ReleaseTable Release { get; set; }
        public string FeatureId { get; set; } = string.Empty;
        public required FeatureTable Feature { get; set; }

    }
}
