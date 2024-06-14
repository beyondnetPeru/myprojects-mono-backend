using AutoMapper;
using Ddd.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyProjects.Domain.ReleaseAggregate;
using MyProjects.Infrastructure.Database;
using MyProjects.Infrastructure.Database.Tables;
using MyProjects.Shared.Application.Extensions;
using MyProjects.Shared.Infrastructure.Database;


namespace MyProjects.Infrastructure.Repositories.Projects
{
    public class ProjectsRepository(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IReleasesRepository
    {
        public async Task<IEnumerable<Release>> GetAll(PaginationDto pagination)
        {
            var queryable = context.Projects.AsQueryable();

            await httpContextAccessor.HttpContext!.AddPaginationInHeader(queryable);

            var data = await queryable.OrderBy(p => p.Name).Paginate(pagination).ToListAsync();

            var projects = mapper.Map<IEnumerable<Release>>(data);

            return projects;
        }

        public async Task<IEnumerable<Release>> GetByName(string name)
        {
            var projectsTable = await context.Projects.Where(x => x.Name.Contains(name)).OrderBy(p => p.Name).ToListAsync();

            var projectsDomain = mapper.Map<IEnumerable<Release>>(projectsTable);

            return projectsDomain;
        }

        public async Task<Release> GetById(string id)
        {
            var projectTable = await context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            var projectDomain = mapper.Map<Release>(projectTable);

            return projectDomain;
        }

        public async Task<bool> Exists(string id)
        {
            return await GetById(id) != null;
        }


        public async Task Create(Release project)
        {
            context.Add(project);
            await context.SaveChangesAsync();
        }


        public async Task Update(Release project)
        {
            context.Update(project);
            await context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            await context.Projects.Where(Projects => Projects.Id == id).ExecuteDeleteAsync();
        }
             
    }
}
