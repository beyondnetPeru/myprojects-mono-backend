using AutoMapper;
using Ddd.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyProjects.Domain.ProjectAggregate;
using MyProjects.Infrastructure.Database;
using MyProjects.Infrastructure.Database.Tables;
using MyProjects.Shared.Application.Extensions;
using MyProjects.Shared.Infrastructure.Database;


namespace MyProjects.Infrastructure.Repositories.Projects
{
    public class ProjectsRepository(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IProjectsRepository
    {
        public async Task<IEnumerable<Project>> GetAll(PaginationDto pagination)
        {
            var queryable = context.Projects.AsQueryable();

            await httpContextAccessor.HttpContext!.AddPaginationInHeader(queryable);

            var data = await queryable.OrderBy(p => p.Name).Paginate(pagination).ToListAsync();

            var projects = mapper.Map<IEnumerable<Project>>(data);

            return projects;
        }

        public async Task<IEnumerable<Project>> GetByName(string name)
        {
            var projectsTable = await context.Projects.Where(x => x.Name.Contains(name)).OrderBy(p => p.Name).ToListAsync();

            var projectsDomain = mapper.Map<IEnumerable<Project>>(projectsTable);

            return projectsDomain;
        }

        public async Task<Project> GetById(string id)
        {
            var projectTable = await context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            var projectDomain = mapper.Map<Project>(projectTable);

            return projectDomain;
        }

        public async Task<bool> Exists(string id)
        {
            return await GetById(id) != null;
        }


        public async Task Create(Project project)
        {
            context.Add(project);
            await context.SaveChangesAsync();
        }


        public async Task Update(Project project)
        {
            context.Update(project);
            await context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            await context.Projects.Where(Projects => Projects.Id == id).ExecuteDeleteAsync();
        }

        public Task<IEnumerable<ProjectVendor>> GetVendors(string projectId)
        {
            var vendors = context.ProjectVendors.Select(x => x.ProjectId == projectId);

            return Task.FromResult(mapper.Map<IEnumerable<ProjectVendor>>(vendors));
        }

        public async Task<ProjectVendor> GetVendorById(string projectId, string vendorId)
        {
            var vendor = await context.ProjectVendors.FirstOrDefaultAsync(x => x.ProjectId == projectId && x.VendorId == vendorId);

            return mapper.Map<ProjectVendor>(vendor);
        }

        public async Task AddVendor(ProjectVendor vendor)
        {
            var projectVendor = mapper.Map<ProjectVendorTable>(vendor);

            context.Add(projectVendor);

            await context.SaveChangesAsync();
        }

        public async Task RemoveVendor(string projectId, string vendorId)
        {
            await context.ProjectVendors.Where(x => x.ProjectId == projectId && x.VendorId == vendorId).ExecuteDeleteAsync();
        }                
    }
}
