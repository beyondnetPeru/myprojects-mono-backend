using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using MyProjects.Projects.Api.Infrastructure.Database;
using MyProjects.Projects.Api.Models;
using MyProjects.Projects.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(configuration =>
        configuration.WithOrigins(
            builder.Configuration["AllowedOrigins"]!).AllowAnyMethod().AllowAnyHeader());
    options.AddPolicy("free", configuration =>
        { configuration.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Projects - Project V1");
});

app.UseCors();

var projects = new List<Project>();

app.MapGet("/projects", [EnableCors(PolicyName = "free")] (IProjectsRepository repository) =>
{
    var projects = repository.GetAll();

    return Results.Ok(projects);
}).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("projects-get"));

app.MapGet("/projects/{id:string}", async (string id, IProjectsRepository repository) =>
{
    var project = await repository.GetById(id);

    if (project == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(project);
});

app.MapPost("/projects", async (Project project, IProjectsRepository repository, IOutputCacheStore outputCacheStore) =>
{
    var id = await repository.Create(project);

    await ClearRefCache(outputCacheStore);

    return Results.Created($"/projects/{id}", project);
});

app.MapPut("/projects", async (string id, Project project, IProjectsRepository repository, IOutputCacheStore outputCacheStore) =>
{
    var exists = await repository.Exists(id);

    if (!exists)
    {
        return Results.BadRequest();
    }

    await repository.Update(project);

    await ClearRefCache(outputCacheStore);

    return Results.Ok(project);

});

app.MapDelete("/project/{id:string}", async (string id, IProjectsRepository repository, IOutputCacheStore outputCacheStore) =>
{
    var exists = await repository.Exists(id);

    if (!exists)
    {
        return Results.BadRequest();
    }

    await repository.Delete(id);

    await ClearRefCache(outputCacheStore);

    return Results.NoContent();
});

static async Task ClearRefCache(IOutputCacheStore outputCacheStore)
{
    await outputCacheStore.EvictByTagAsync("projects-get", default);
}


app.Run();

