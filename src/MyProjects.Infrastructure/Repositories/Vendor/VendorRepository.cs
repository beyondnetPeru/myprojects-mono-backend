using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyProjects.Domain.VendorAggregate;
using MyProjects.Infrastructure.Database;
using MyProjects.Shared.Application.Extensions;
using MyProjects.Shared.Infrastructure.Database;

namespace MyProjects.Infrastructure.Repositories.Vendor
{
    public class VendorRepository(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IVendorRepository
    {
        public async Task<IEnumerable<Domain.VendorAggregate.Vendor>> GetAll(Shared.Application.Pagination.PaginationDto pagination)
        {
            var queryable = context.Vendors.AsQueryable();

            await httpContextAccessor.HttpContext!.AddPaginationInHeader(queryable);

            var data = await queryable.OrderBy(v => v.Name).Paginate(pagination).ToListAsync();

            var vendors = mapper.Map<IEnumerable<Domain.VendorAggregate.Vendor>>(data);

            return vendors;
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
