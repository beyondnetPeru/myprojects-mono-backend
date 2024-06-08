namespace MyProjects.Infrastructure.Database.Tables
{
    public class ProjectTable
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public ICollection<ProjectVendorTable>? Vendors { get; set; }
        public int Status { get; set; } = 1;
    }
}
