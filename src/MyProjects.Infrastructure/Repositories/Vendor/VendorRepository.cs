using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyProjects.Domain.VendorAggregate;
using MyProjects.Infrastructure.Database;

namespace MyProjects.Infrastructure.Repositories.Vendor
{
    public class VendorRepository(ApplicationDbContext context, IMapper mapper) : IVendorRepository
    {

        public async Task<IEnumerable<Domain.VendorAggregate.Vendor>> GetAll()
        {
            var vendorsTable = await context.Vendors.ToListAsync();

            var vendorsDomain = mapper.Map<IEnumerable<Domain.VendorAggregate.Vendor>>(vendorsTable);

            return vendorsDomain;
        }

        public async Task<Domain.VendorAggregate.Vendor> GetById(string id)
        {
            var vendorTable = await context.Vendors.FirstOrDefaultAsync(x => x.Id == id);

            var vendorDomain = mapper.Map<Domain.VendorAggregate.Vendor>(vendorTable);

            return vendorDomain;
        }

        public async Task<bool> Exists(string id)
        {
            return await GetById(id) != null;
        }

        public Task Create(Domain.VendorAggregate.Vendor vendor)
        {
            context.Add(vendor);

            return context.SaveChangesAsync();
        }


        public Task Update(Domain.VendorAggregate.Vendor vendor)
        {
            context.Update(vendor);

            return context.SaveChangesAsync();
        }

        public Task Delete(string id)
        {
            return context.Vendors.Where(vendor => vendor.Id == id).ExecuteDeleteAsync();
        }
    }
}
