using Microsoft.EntityFrameworkCore;
using MyProjects.Domain.ProjectAggregate;
using MyProjects.Infrastructure.Database;
using MyProjects.Projects.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("name=DefaultConnection"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(configuration =>
    {
        configuration.WithOrigins(builder.Configuration["allowedOrigins"]!).AllowAnyMethod()
        .AllowAnyHeader();
    });

    options.AddPolicy("free", configuration =>
    {
        configuration.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddOutputCache();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors();

app.UseOutputCache();

app.MapGroup("/projects").MapProjects();

app.Run();

