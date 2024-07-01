namespace MyProjects.Infrastructure.Database.Tables
{
    public class ProjectFeatureTable
    {
        public string ProjectId { get; set; } = string.Empty;
        public required ProjectTable Project { get; set; }
        public string FeatureId { get; set; } = string.Empty;
        public required FeatureTable Feature { get; set; }

    }
}
