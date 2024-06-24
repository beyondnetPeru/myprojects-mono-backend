namespace MyProjects.Application.Release.Dtos.Release
{
    public class CreateReleaseFeatureDto
    {
        public string ReleaseId { get; set; } = string.Empty;
        public string FeatureId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
