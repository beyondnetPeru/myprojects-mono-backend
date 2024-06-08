using Microsoft.AspNetCore.Http;


namespace MyProjects.Application.Dtos.Project
{
    public class CreateProjectDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
