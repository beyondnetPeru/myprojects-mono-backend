namespace MyProjects.Domain.VendorAggregate
{
    public class Vendor
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;

        public int Status { get; set; } = 1;
    }
}
