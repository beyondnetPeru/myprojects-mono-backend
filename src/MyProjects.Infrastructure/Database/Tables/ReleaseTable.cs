namespace MyProjects.Infrastructure.Database.Tables
{
    public class ReleaseTable
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime GoLiveDate { get; set; }
        public string Owner { get; set; } = string.Empty;
        public ICollection<ReleaseFeatureTable>? Features { get; set; }
        public ICollection<ReleaseReferenceTable>? References { get; set; }
        public ICollection<ReleaseCommentTable>? Comments { get; set; }
        public int Status { get; set; } = 1;
    }
}
