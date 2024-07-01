namespace MyProjects.Infrastructure.Database.Tables
{
    public class ProjectTable
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime GoLiveDate { get; set; }
        public string Owner { get; set; } = string.Empty;
        public ICollection<ProjectFeatureTable>? Features { get; set; }
        public ICollection<ProjectReferenceTable>? References { get; set; }
        public ICollection<ProjectCommentTable>? Comments { get; set; }
        public int Status { get; set; } = 1;
    }
}
