namespace MyProjects.Infrastructure.Database.Tables
{
    public class ProjectCommentTable
    {
        public string ProjectId { get; set; } = string.Empty;
        public required ProjectTable Project { get; set; }
        public string CommentId { get; set; } = string.Empty;
        public required CommentTable Comment { get; set; }
    }
}
