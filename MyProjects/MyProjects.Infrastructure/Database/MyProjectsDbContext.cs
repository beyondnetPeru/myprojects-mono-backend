using Microsoft.EntityFrameworkCore;
using MyProjects.Infrastructure.Database.Tables;

namespace MyProjects.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectTable>().HasKey(p => p.Id);
            modelBuilder.Entity<ProjectTable>().Property(p => p.Name).HasMaxLength(250).IsRequired();
            modelBuilder.Entity<ProjectTable>().Property(p => p.Picture).IsUnicode();
            modelBuilder.Entity<ProjectTable>().HasMany(o => o.Vendors).WithOne().HasForeignKey(v => v.ProjectId);
            modelBuilder.Entity<ProjectTable>().HasMany(o => o.Tasks).WithOne().HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<VendorTable>().HasKey(p => p.Id);
            modelBuilder.Entity<TaskTable>().HasKey(p => p.Id);
        }

        public DbSet<ProjectTable> Projects { get; set; }
        public DbSet<VendorTable> Vendors { get; set; }
        public DbSet<TaskTable> Tasks { get; set; }
    }
}
