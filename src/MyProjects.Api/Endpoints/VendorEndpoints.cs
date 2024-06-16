using AutoMapper;
using Ddd.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MyProjects.Application.Release.Dtos.Vendor;
using MyProjects.Domain.VendorAggregate;

namespace MyProjects.Api.Endpoints
{
    public static class VendorEndpoints
    {
        public static RouteGroupBuilder MapVendors(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAllAsync)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("vendors-get"));

            group.MapGet("/{id}", GetByIdAsync);

            group.MapPost("/", CreateAsync);

            group.MapPut("/{id}", UpdateAsync);

            group.MapDelete("/{id}", DeleteAsync);

            return group;
        }

        public static async Task<Ok<List<VendorDto>>> GetAllAsync(IVendorRepository repository, IMapper mapper, int page=1, int recordsPerPage=10)
        {
            var pagination = new PaginationDto { Page = page, RecordsPerPage = recordsPerPage };

            var vendors = await repository.GetAll(pagination);

            var dtos = mapper.Map<List<VendorDto>>(vendors);

            return TypedResults.Ok(dtos);
        }

        public static async Task<Results<Ok<VendorDto>, NotFound>> GetByIdAsync(string id, IVendorRepository repository, IMapper mapper)
        {
            var vendor = await repository.GetById(id);

            if (vendor == null)
            {
                return TypedResults.NotFound();
            }

            var vendorDto = mapper.Map<VendorDto>(vendor);

            return TypedResults.Ok(vendorDto);
        }

        public static async Task<Results<Created<VendorDto>, BadRequest>> CreateAsync(CreateVendorDto vendorDto, IVendorRepository repository, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var vendor = mapper.Map<Vendor>(vendorDto);

            vendor.Id = Guid.NewGuid().ToString();

            await repository.Create(vendor);

            var dto = mapper.Map<VendorDto>(vendor);

            await ClearRefCache(outputCacheStore);

            return TypedResults.Created($"/{vendor.Id}", dto);
        }

        public static async Task<Results<Ok<VendorDto>, NotFound>> UpdateAsync(string id, UpdateVendorDto vendorDto, IVendorRepository repository, IMapper mapper)
        {
            var vendor = await repository.GetById(id);

            if (vendor == null)
            {
                return TypedResults.NotFound();
            }

            vendor = mapper.Map(vendorDto, vendor);

            await repository.Update(vendor);

            var dto = mapper.Map<VendorDto>(vendor);

            return TypedResults.Ok(dto);
        }


        public static async Task<Results<NoContent, NotFound>> DeleteAsync(string id, IVendorRepository repository)
        {
            var vendor = await repository.GetById(id);

            if (vendor == null)
            {
                return TypedResults.NotFound();
            }

            await repository.Delete(id);

            return TypedResults.NoContent();
        }

        static async Task ClearRefCache(IOutputCacheStore outputCacheStore)
        {
            await outputCacheStore.EvictByTagAsync("vendors-get", default);
        }
    }
}
