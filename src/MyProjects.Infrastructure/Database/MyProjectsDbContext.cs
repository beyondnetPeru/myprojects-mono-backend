using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProjects.Infrastructure.Database.Tables;

namespace MyProjects.Infrastructure.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ReleaseTable> Releases { get; set; }
        public DbSet<ReleaseFeatureTable> ReleaseFeatures { get; set; }
        public DbSet<ReleaseReferenceTable> ReleaseReferences { get; set; }
        public DbSet<ReleaseCommentTable> ReleaseComments { get; set; }
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


            modelBuilder.Entity<ReleaseTable>().HasKey(p => p.Id);
            modelBuilder.Entity<ReleaseTable>().Property(p => p.Id).HasMaxLength(36);


            modelBuilder.Entity<ReleaseFeatureTable>().HasKey(p => new { p.ReleaseId, p.FeatureId});
            
            modelBuilder.Entity<ReleaseFeatureTable>()
                        .HasOne(p => p.Release)
                        .WithMany(v => v.Features)
                        .HasForeignKey(p => p.ReleaseId);

            modelBuilder.Entity<ReleaseReferenceTable>()
                        .HasOne(v => v.Release)
                        .WithMany(p => p.References)
                        .HasForeignKey(v => v.ReleaseId);

            modelBuilder.Entity<ReleaseCommentTable>()
                        .HasOne(v => v.Release)
                        .WithMany(p => p.Comments)
                        .HasForeignKey(v => v.ReleaseId);


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
