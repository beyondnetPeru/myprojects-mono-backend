using Ddd;
using Ddd.ValueObjects;

namespace MyProjects.Domain.ProjectAggregate
{
    public class ProjectComment : Entity<ProjectComment>
    {
        public StringValueObject Text { get; set; }
        public DateTimeValueObject Date { get; set; }

        private ProjectComment(StringValueObject text, DateTimeValueObject date)
        {
            Text = text;
            Date = date;
        }


        public static ProjectComment Create(StringValueObject text, DateTimeValueObject date)
        {
            return new ProjectComment(text, date);
        }

        public void Update(StringValueObject text, DateTimeValueObject date)
        {
            Text = text;
            Date = date;
        }
    }
}
