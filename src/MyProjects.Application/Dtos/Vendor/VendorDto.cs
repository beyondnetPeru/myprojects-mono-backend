namespace MyProjects.Application.Dtos.Vendor
{
    public class VendorDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int Status { get; set; }
    }
}
