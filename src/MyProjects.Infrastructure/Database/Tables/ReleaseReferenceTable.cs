namespace MyProjects.Infrastructure.Database.Tables
{
    public class ReleaseReferenceTable
    {
        public string ReleaseId { get; set; } = string.Empty;
        public required ReleaseTable Release { get; set; }
        public string ReferenceId { get; set; } = string.Empty;
        public required ReferenceTable  Reference { get; set; }
    }
}
