namespace MyProjects.Infrastructure.Database.Tables
{
    public class ProjectReferenceTable
    {
        public string ProjectId { get; set; } = string.Empty;
        public required ProjectTable Project { get; set; }
        public string ReferenceId { get; set; } = string.Empty;
        public required ReferenceTable  Reference { get; set; }
    }
}
