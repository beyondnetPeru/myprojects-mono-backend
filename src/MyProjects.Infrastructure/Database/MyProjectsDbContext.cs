using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProjects.Infrastructure.Database.Tables;

namespace MyProjects.Infrastructure.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ProjectTable> Projects { get; set; }
        public DbSet<ProjectFeatureTable> ProjectFeatures { get; set; }
        public DbSet<ProjectReferenceTable> ProjectReferences { get; set; }
        public DbSet<ProjectCommentTable> ProjectComments { get; set; }
        public DbSet<FeatureTable> Features { get; set; }
        public DbSet<FeaturePhaseTable> FeaturePhases { get; set; }
        public DbSet<FeatureCommentTable> FeatureComments { get; set; }
        public DbSet<FeatureRolloutTable> FeatureRollouts { get; set; }
        public DbSet<ReferenceTable> References { get; set; }
        public DbSet<CommentTable> Comments { get; set; }
        public DbSet<PhaseTable> Phases { get; set; }
        public DbSet<RolloutTable> Rollouts { get; set; }


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


            modelBuilder.Entity<ProjectFeatureTable>().HasKey(p => new { p.ProjectId, p.FeatureId});
            
            modelBuilder.Entity<ProjectFeatureTable>()
                        .HasOne(p => p.Project)
                        .WithMany(v => v.Features)
                        .HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<ProjectReferenceTable>()
                        .HasOne(v => v.Project)
                        .WithMany(p => p.References)
                        .HasForeignKey(v => v.ProjectId);

            modelBuilder.Entity<ProjectCommentTable>()
                        .HasOne(v => v.Project)
                        .WithMany(p => p.Comments)
                        .HasForeignKey(v => v.ProjectId);


            modelBuilder.Entity<FeatureTable>().HasKey(p => p.Id);
            modelBuilder.Entity<FeatureTable>().Property(p => p.Id).HasMaxLength(36);

            modelBuilder.Entity<FeaturePhaseTable>()
                        .HasOne(p => p.Feature)
                        .WithMany(v => v.Phases)
                        .HasForeignKey(p => p.FeatureId);

            modelBuilder.Entity<FeatureCommentTable>()
                    .HasOne(v => v.Feature)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(v => v.FeatureId);

            modelBuilder.Entity<FeatureRolloutTable>()
                    .HasOne(v => v.Feature)
                    .WithMany(p => p.Rollouts)
                    .HasForeignKey(v => v.FeatureId);


            modelBuilder.Entity<ReferenceTable>().HasKey(p => p.Id);
            modelBuilder.Entity<ReferenceTable>().Property(p => p.Id).HasMaxLength(36);

            modelBuilder.Entity<CommentTable>().HasKey(p => p.Id);
            modelBuilder.Entity<CommentTable>().Property(p => p.Id).HasMaxLength(36);

            modelBuilder.Entity<PhaseTable>().HasKey(p => p.Id);
            modelBuilder.Entity<PhaseTable>().Property(p => p.Id).HasMaxLength(36);

            modelBuilder.Entity<RolloutTable>().HasKey(p => p.Id);
            modelBuilder.Entity<RolloutTable>().Property(p => p.Id).HasMaxLength(36);
        }
    }
}
