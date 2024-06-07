using AutoMapper;
using MyProjects.Application.Dtos.Vendor;
using MyProjects.Domain.VendorAggregate;

namespace MyProjects.Application.Mappers
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Vendor, VendorDto>();
            CreateMap<VendorDto, Vendor>();
            CreateMap<CreateVendorDto, Vendor>();
            CreateMap<UpdateVendorDto, VendorDto>();
        }
    }
}
