
namespace MyProjects.Application.Release.Dtos.Release
{
    public class CommentDto
    {
        public string Id { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
