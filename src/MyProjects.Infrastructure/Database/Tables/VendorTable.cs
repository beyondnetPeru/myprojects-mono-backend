namespace MyProjects.Infrastructure.Database.Tables
{
    public class VendorTable
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ProjectId { get; set; }
        public int Status { get; set; } = 1;
    }
}
