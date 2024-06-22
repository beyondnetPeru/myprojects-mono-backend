
using Ddd;
using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseFeatureRollout : Entity<ReleaseFeatureRollout>
    {
        public StringValueObject Country { get; set; }
        public DateTimeValueObject RegisterDate { get; set; }

        private ReleaseFeatureRollout(StringValueObject country, DateTimeValueObject date)
        {
            Country = country;
            RegisterDate = date;
        }

        public static ReleaseFeatureRollout Create(StringValueObject country, DateTimeValueObject date)
        {
            return new ReleaseFeatureRollout(country, date);
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
