using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyProjects.Domain.ReleaseAggregate;
using MyProjects.Infrastructure.Database;
using MyProjects.Projects.Api.Endpoints;
using MyProjects.Shared.Infrastructure.FileStorage;
using Myreleases.Infrastructure.Repositories.releases;


var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("name=DefaultConnection"));

builder.Services.AddIdentityCore<IdentityUser>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();

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
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddScoped<IReleasesRepository, ReleasesRepository>();
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

// Run
app.Run();

