using AutoMapper;
using MyProjects.Application.Release.Dtos.Vendor;
using MyProjects.Domain.VendorAggregate;

namespace MyProjects.Application.Release.Mappers
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
