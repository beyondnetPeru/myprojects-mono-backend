
namespace MyProjects.Application.Release.Dtos.Release
{
    public class ProjectCommentDto
    {
        public string Id { get; set; } = string.Empty;
        public string ReleaseId { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
