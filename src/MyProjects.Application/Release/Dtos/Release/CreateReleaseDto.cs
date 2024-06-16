namespace MyProjects.Application.Release.Dtos.Release
{
    public class CreateReleaseDto
    {
        public string Title { get; private set; } 
        public string? Description { get; private set; }

        public CreateReleaseDto(string title, string? description)
        {
            Title = title;
            Description = description;
        }
    }
}
