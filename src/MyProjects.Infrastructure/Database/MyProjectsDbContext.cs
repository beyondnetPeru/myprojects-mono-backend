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

            modelBuilder.Entity<ProjectTable>().HasMany(p => p.Vendors).WithMany(p => p.Projects)
                .UsingEntity("ProjectVendors",
                v => v.HasOne(typeof(VendorTable)).WithMany().HasForeignKey("VendorId").HasPrincipalKey(nameof(VendorTable.Id)),
                p => p.HasOne(typeof(ProjectTable)).WithMany().HasForeignKey("ProjectId").HasPrincipalKey(nameof(ProjectTable.Id)),
                k => k.HasKey("ProjectId", "VendorId"));


            modelBuilder.Entity<VendorTable>().HasKey(p => p.Id);
            modelBuilder.Entity<VendorTable>().Property(p => p.Id).HasMaxLength(36);

        }


        public DbSet<ProjectTable> Projects { get; set; }
        public DbSet<VendorTable> Vendors { get; set; }

    }
}
