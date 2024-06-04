using Microsoft.EntityFrameworkCore;
using MyProjects.Projects.Api.Models;

namespace MyProjects.Projects.Api.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().Property(p => p.Name).HasMaxLength(250).IsRequired();
        }

        public DbSet<Project> Projects { get; set; }
    }
}
