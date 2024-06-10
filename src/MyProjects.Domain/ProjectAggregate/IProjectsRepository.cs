using Ddd.Interfaces;

namespace MyProjects.Domain.ProjectAggregate
{
    public interface IProjectsRepository : IRepository<Project>
    {
        Task<IEnumerable<ProjectVendor>> GetVendors(string projectId);
        Task<ProjectVendor> GetVendorById(string projectId, string vendorId);
        Task AddVendor(ProjectVendor item);
        Task RemoveVendor(string projectId, string vendorId);
        Task<IEnumerable<Project>> GetByName(string name);
    }
}
