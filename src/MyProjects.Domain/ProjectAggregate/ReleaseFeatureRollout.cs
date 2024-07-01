
using Ddd;
using Ddd.ValueObjects;

namespace MyProjects.Domain.ProjectAggregate
{
    public class ProjectFeatureRollout : Entity<ProjectFeatureRollout>
    {
        public StringValueObject Country { get; set; }
        public DateTimeValueObject RegisterDate { get; set; }

        private ProjectFeatureRollout(StringValueObject country, DateTimeValueObject date)
        {
            Country = country;
            RegisterDate = date;
        }

        public static ProjectFeatureRollout Create(StringValueObject country, DateTimeValueObject date)
        {
            return new ProjectFeatureRollout(country, date);
        }

        public void UpdateCountry(StringValueObject country)
        {
            Country = country;
        }

        public void UpdateDate(DateTimeValueObject date)
        {
            RegisterDate = date;
        }
    }
}
