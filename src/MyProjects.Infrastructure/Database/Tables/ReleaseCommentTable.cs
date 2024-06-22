namespace MyProjects.Infrastructure.Database.Tables
{
    public class ReleaseCommentTable
    {
        public string ReleaseId { get; set; } = string.Empty;
        public required ReleaseTable Release { get; set; }
        public string CommentId { get; set; } = string.Empty;
        public required CommentTable Comment { get; set; }
    }
}
