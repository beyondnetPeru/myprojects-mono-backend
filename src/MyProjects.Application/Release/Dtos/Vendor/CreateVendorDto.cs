namespace MyProjects.Application.Release.Dtos.Vendor
{
    public class CreateVendorDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }

    }
}
