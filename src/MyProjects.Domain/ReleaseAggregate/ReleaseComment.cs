using Ddd;
using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseComment : Entity<ReleaseComment>
    {
        public StringValueObject Text { get; set; }
        public DateTimeValueObject Date { get; set; }

        private ReleaseComment(StringValueObject text, DateTimeValueObject date)
        {
            Text = text;
            Date = date;
        }


        public static ReleaseComment Create(StringValueObject text, DateTimeValueObject date)
        {
            return new ReleaseComment(text, date);
        }

        public void Update(StringValueObject text, DateTimeValueObject date)
        {
            Text = text;
            Date = date;
        }
    }
}
