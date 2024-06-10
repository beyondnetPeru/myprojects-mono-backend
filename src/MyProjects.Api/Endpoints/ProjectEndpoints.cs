
using AutoMapper;
using Ddd.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MyProjects.Application.Dtos.Project;
using MyProjects.Application.Dtos.Vendor;
using MyProjects.Domain.ProjectAggregate;
using MyProjects.Shared.Infrastructure.FileStorage;


namespace MyProjects.Projects.Api.Endpoints
{
    public static class ProjectEndpoints
    {
        private readonly static string _container = "projects";

        public static RouteGroupBuilder MapProjects(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAllAsync)
                .CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("projects-get"));

            group.MapGet("/name/{name}", GetByNameAsync).RequireAuthorization();

            group.MapGet("/{id}", GetByIdAsync);

            group.MapPost("/", CreateAsync);

            group.MapPut("/{id}", UpdateAsync);

            group.MapDelete("/{id}", DeleteAsync);

            group.MapGet("/{projectId}/vendors", GetVendorsAsync);

            group.MapGet("/{projectId}/vendors/{vendorId}", GetVendorByIdAsync);

            group.MapPost("/{projectId}/vendors", AddVendorAsync);

            group.MapDelete("/{projectId}/vendors/{vendorId}", RemoveVendorAsync);


            return group;
        }

        public static async Task<Ok<List<ProjectDto>>> GetAllAsync(IProjectsRepository repository, IMapper mapper, int page=1, int recordsPerPage = 10)
        {
            var pagination = new PaginationDto { Page = page, RecordsPerPage = recordsPerPage }; 

            var projects = await repository.GetAll(pagination);

            var dtos = mapper.Map<List<ProjectDto>>(projects);

            return TypedResults.Ok(dtos);
        }

        public static async Task<Ok<List<ProjectDto>>> GetByNameAsync(string name, IProjectsRepository repository, IMapper mapper)
        {
            var projects = await repository.GetByName(name);

            var dtos = mapper.Map<List<ProjectDto>>(projects);

            return TypedResults.Ok(dtos);
        }

        public static async Task<Results<Ok<ProjectDto>, NotFound>> GetByIdAsync(string id, IProjectsRepository repository, IMapper mapper)
        {
            var project = await repository.GetById(id);

            if (project == null)
            {
                return TypedResults.NotFound();
            }


            var projectDto = mapper.Map<ProjectDto>(project);

            return TypedResults.Ok(projectDto);
        }

        public static async Task<Results<Created<ProjectDto>, ValidationProblem>> CreateAsync(CreateProjectDto projectDto, IProjectsRepository repository,
                                                                                       IOutputCacheStore outputCacheStore, IMapper mapper, IFileStorage fileStorage, 
                                                                                       IValidator<CreateProjectDto> validator)
        {
            var validationResult = await validator.ValidateAsync(projectDto);

            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }

            var project = mapper.Map<Project>(projectDto);

            if (projectDto.Image != null)
            {
                var image = await fileStorage.Store(_container, projectDto.Image);

                project.Image = image;
            }

            project.Id = Guid.NewGuid().ToString();


            await repository.Create(project);

            var dto = mapper.Map<ProjectDto>(project);

            await ClearRefCache(outputCacheStore);

            return TypedResults.Created($"/{project.Id}", dto);
        }

        public static async Task<Results<Ok<ProjectDto>, BadRequest>> UpdateAsync(string id, UpdateProjectDto projectDto, IProjectsRepository repository, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var exists = await repository.Exists(id);

            if (!exists)
            {
                return TypedResults.BadRequest();
            }

            var project = mapper.Map<Project>(projectDto);

            project.Id = id;

            await repository.Update(project);

            var dto = mapper.Map<ProjectDto>(project);

            await ClearRefCache(outputCacheStore);

            return TypedResults.Ok(dto);
        }

        public static async Task<Results<NoContent, BadRequest>> DeleteAsync(string id, IProjectsRepository repository, IOutputCacheStore outputCacheStore)
        {
            var exists = await repository.Exists(id);

            if (!exists)
            {
                return TypedResults.BadRequest();
            }

            await repository.Delete(id);

            await ClearRefCache(outputCacheStore);

            return TypedResults.NoContent();
        }

        public static async Task<Ok<List<VendorDto>>> GetVendorsAsync(string projectId, IProjectsRepository repository, IMapper mapper)
        {
            var vendors = await repository.GetVendors(projectId);

            var vendorsDto = mapper.Map<List<VendorDto>>(vendors);

            return TypedResults.Ok(vendorsDto);
        }

        public static async Task<Results<Ok<VendorDto>, NotFound>> GetVendorByIdAsync(string projectId, string vendorId, IProjectsRepository repository, IMapper mapper)
        {
            var vendor = await repository.GetVendorById(projectId, vendorId);

            if (vendor == null)
            {
                return TypedResults.NotFound();
            }

            var vendorDto = mapper.Map<VendorDto>(vendor);

            return TypedResults.Ok(vendorDto);
        }

        public static async Task<Results<Created<VendorDto>, BadRequest>> AddVendorAsync(string projectId, CreateVendorDto vendorDto, IProjectsRepository repository, IMapper mapper)
        {
            var vendor = mapper.Map<ProjectVendor>(vendorDto);

            vendor.ProjectId = projectId;

            await repository.AddVendor(vendor);

            var dto = mapper.Map<VendorDto>(vendor);

            return TypedResults.Created($"/{projectId}/vendors/{vendor.VendorId}", dto);
        }

        public static async Task<Results<NoContent, BadRequest>> RemoveVendorAsync(string projectId, string vendorId, IProjectsRepository repository)
        {
            await repository.RemoveVendor(projectId, vendorId);

            return TypedResults.NoContent();
        }

        static async Task ClearRefCache(IOutputCacheStore outputCacheStore)
        {
            await outputCacheStore.EvictByTagAsync("projects-get", default);
        }
    }
}
