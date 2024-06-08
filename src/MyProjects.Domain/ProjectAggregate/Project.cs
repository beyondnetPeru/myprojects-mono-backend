
using MyProjects.Shared.Infrastructure.FileStorage;

namespace MyProjects.Domain.ProjectAggregate
{
    public class Project
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Image{ get; set; }
        public IEnumerable<ProjectVendor>? Vendors { get; set; }
        public int Status { get; set; } = 1;
    }
}
