using AutoMapper;
using MyProjects.Application.Release.Dtos.Release;
using MyProjects.Application.Release.UseCases.Release.Commands;
using MyProjects.Domain.ReleaseAggregate;


namespace MyProjects.Application.Release.Mappers
{
    public class ReleaseProfile : Profile
    {
        public ReleaseProfile()
        {
            CreateMap<CreateReleaseDto, CreateReleaseCommand>();
            CreateMap<UpdateReleaseDto, UpdateReleaseCommand>();
            CreateMap<ReleaseEntity, ReleaseDto>();

        }
    }
}
