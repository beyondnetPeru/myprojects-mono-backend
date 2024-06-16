namespace MyProjects.Application.Release.Dtos.Release
{
    public class ReleaseDto
    {
        public string Id { get; private set; } 
        public string Title { get; private set; } 
        public string? Description { get; private set; }
        public string Status { get;  private set; }

        public ReleaseDto(string id, string title, string? description, string status)
        {
            Id = id;
            Title = title;
            Description = description;
            Status = status;
        }
    }
}
