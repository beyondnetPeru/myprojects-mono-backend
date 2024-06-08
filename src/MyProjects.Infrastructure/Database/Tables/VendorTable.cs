namespace MyProjects.Infrastructure.Database.Tables
{
    public class VendorTable
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }

        public ICollection<ProjectVendorTable>? Projects { get; set; }
        public int Status { get; set; } = 1;
    }
}
