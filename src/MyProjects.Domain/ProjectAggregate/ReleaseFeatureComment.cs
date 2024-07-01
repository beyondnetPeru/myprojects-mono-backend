using Ddd;
using Ddd.ValueObjects;

namespace MyProjects.Domain.ProjectAggregate
{
    public  class ProjectFeatureComment : Entity<ProjectFeatureComment>
    {
        public StringValueObject Comment { get; set; } 
        public StringValueObject Author { get; set; }
        public StringValueObject Date { get; set; }

        private ProjectFeatureComment(StringValueObject comment, StringValueObject author, StringValueObject date)
        {
            Comment = comment;
            Author = author;
            Date = date;
        }

        public static ProjectFeatureComment Create(StringValueObject comment, StringValueObject author, StringValueObject date)
        {
            return new ProjectFeatureComment(comment, author, date);
        }

        public void UpdateComment(StringValueObject comment)
        {
            Comment = comment;
        }

        public void UpdateAuthor(StringValueObject author)
        {
            Author = author;
        }

        public void UpdateDate(StringValueObject date)
        {
            Date = date;
        }
    }
}
