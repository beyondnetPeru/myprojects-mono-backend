using Microsoft.EntityFrameworkCore;
using MyProjects.Domain.ProjectAggregate;

namespace MyProjects.Infrastructure.Database
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ApplicationDbContext context;

        public ProjectsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Create(Project project)
        {
            context.Add(project);
            await context.SaveChangesAsync();
            return project.Id;
        }

        public async Task Delete(string id)
        {
            await context.Projects.Where(Projects => Projects.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> Exists(string id)
        {
            return await GetById(id) != null;
        }

        public async Task<Project?> GetById(string id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            return null;
        }

        public async System.Threading.Tasks.Task Update(Project project)
        {
            context.Update(project);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            var data = await context.Projects.ToListAsync();

            return (IEnumerable<Project>)data.AsQueryable().OrderBy(p => p.Name);

        }
    }
}
