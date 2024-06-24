
namespace MyProjects.Application.Release.Dtos.Release
{
    public class ChangeReleaseTitleDto
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public ChangeReleaseTitleDto(string id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
