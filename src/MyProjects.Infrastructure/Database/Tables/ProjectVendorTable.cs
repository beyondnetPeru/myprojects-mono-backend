namespace MyProjects.Infrastructure.Database.Tables
{
    public class ProjectVendorTable
    {
        public string ProjectId { get; set; } = string.Empty;
        public required ProjectTable Project { get; set; }
        public string VendorId { get; set; } = string.Empty;
        public required VendorTable Vendor { get; set; }

    }
}
