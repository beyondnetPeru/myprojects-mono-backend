using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyProjects.Api.Endpoints;
using MyProjects.Domain.ProjectAggregate;
using MyProjects.Domain.VendorAggregate;
using MyProjects.Infrastructure.Database;
using MyProjects.Infrastructure.Repositories.Vendor;
using MyProjects.Projects.Api.Endpoints;
using MyProjects.Shared.Infrastructure.FileStorage;


var builder = WebApplication.CreateBuilder(args);

// Services

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
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddProblemDetails();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

// Inmplementations
builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddTransient<IFileStorage, LocalFileStorage>(); // TODO: Realocate to Infrastructure based on cloud
builder.Services.AddHttpContextAccessor();//Complement for LocalFileStorage with wwwroot

var app = builder.Build();

// Middlewares
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles(); //Complement for LocalFileStorage with AddHttpContextAccessor
app.UseCors();
app.UseOutputCache();
app.UseAuthorization();

// Endpoints
app.MapGroup("/projects").MapProjects();
app.MapGroup("/vendors").MapVendors();

// Run
app.Run();

