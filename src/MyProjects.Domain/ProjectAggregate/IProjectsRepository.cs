using MyProjects.Shared.Domain;

namespace MyProjects.Domain.ProjectAggregate
{
    public interface IProjectsRepository : IRepository<Project>
    {
        Task<IEnumerable<ProjectVendor>> GetVendors(string projectId);
        Task AddVendor(ProjectVendor vendor);
        Task RemoveVendor(string projectId, string vendorId);
    }
}
