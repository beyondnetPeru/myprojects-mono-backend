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
            modelBuilder.Entity<ProjectTable>().Property(p => p.Id).HasMaxLength(36);
            modelBuilder.Entity<ProjectTable>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<ProjectTable>().Property(p => p.Picture).IsUnicode();



            modelBuilder.Entity<ProjectVendorTable>().HasKey(p => new { p.ProjectId, p.VendorId });
            modelBuilder.Entity<ProjectVendorTable>()
                        .HasOne(p => p.Project)
                        .WithMany(v => v.Vendors)
                        .HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<ProjectVendorTable>()
                        .HasOne(v => v.Vendor)
                        .WithMany(p => p.Projects)
                        .HasForeignKey(v => v.VendorId);


            modelBuilder.Entity<VendorTable>().HasKey(p => p.Id);
            modelBuilder.Entity<VendorTable>().Property(p => p.Id).HasMaxLength(36);

        }


        public DbSet<ProjectTable> Projects { get; set; }
        public DbSet<VendorTable> Vendors { get; set; }
        public DbSet<ProjectVendorTable> ProjectVendors { get; set; }

    }
}
