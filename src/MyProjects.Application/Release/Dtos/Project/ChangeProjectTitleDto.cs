
namespace MyProjects.Application.Release.Dtos.Release
{
    public class ChangeProjectTitleDto
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public ChangeProjectTitleDto(string id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
