namespace MyProjects.Infrastructure.Database.Tables
{
    public class FeatureCommentTable
    {
        public string FeatureId { get; set; } = string.Empty;
        public required FeatureTable Feature { get; set; }
        public string CommentId { get; set; } = string.Empty;
        public required CommentTable Comment { get; set; }
    }
}
