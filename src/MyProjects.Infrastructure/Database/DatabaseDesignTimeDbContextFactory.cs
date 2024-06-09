using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyProjects.Infrastructure.Database
{
    /*
     A factory for creating derived DbContext instances. 
    Implement this interface to enable design-time services 
    for context types that do not have a public default constructor.
    At design-time, derived DbContext instances can be created 
    in order to enable specific design-time experiences such 
    as Migrations.
 
    Design-time services will automatically discover 
    implementations of this interface that are in the 
    startup assembly or the same assembly as the derived context.
     */
    public class DatabaseDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new ApplicationDbContext(builder.Options);
        }
    }
}
