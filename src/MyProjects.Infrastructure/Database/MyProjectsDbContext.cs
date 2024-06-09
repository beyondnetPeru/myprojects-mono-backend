using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProjects.Infrastructure.Database.Tables;

namespace MyProjects.Infrastructure.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ProjectTable> Projects { get; set; }
        public DbSet<VendorTable> Vendors { get; set; }
        public DbSet<ProjectVendorTable> ProjectVendors { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");


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



    }
}
