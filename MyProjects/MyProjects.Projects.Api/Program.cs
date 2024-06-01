using Microsoft.AspNetCore.Cors;
using MyProjects.Projects.Api.Models;

var builder = WebApplication.CreateBuilder(args);

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


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors();

var projects = new List<Project>();

app.MapGet("/projects/all", [EnableCors(PolicyName = "free")] () =>
{
    projects = new List<Project>
    {
        new Project { Id = "1", Name = "Project 1", Description="", Status = 1 },
        new Project { Id = "2", Name = "Project 2", Description="", Status = 1 },
        new Project { Id = "3", Name = "Project 3", Description="", Status = 1 }
    };

    return projects;
});

app.MapGet("/projects/byid", (string id) =>
{
    return projects.FirstOrDefault(x => x.Id == id);
});

app.MapPost("/projects", (Project project) =>
{
    projects.Add(project);
});

app.MapPut("/projects", (string id, Project project) =>
{

});

app.MapDelete("/project", () => "Hello World!");



app.Run();
