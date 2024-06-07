using AutoMapper;
using Microsoft.EntityFrameworkCore;

using MyProjects.Domain.ProjectAggregate;


namespace MyProjects.Infrastructure.Database
{
    public class ProjectsRepository(ApplicationDbContext context, IMapper mapper) : IProjectsRepository
    {

        public async Task<IEnumerable<Project>> GetAll()
        {
            var prjectsTable = await context.Projects.ToListAsync();

            var projectsDomain = mapper.Map<IEnumerable<Project>>(prjectsTable);

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
    }
}
