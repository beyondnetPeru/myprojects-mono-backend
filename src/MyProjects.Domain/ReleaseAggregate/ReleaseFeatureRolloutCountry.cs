using Ddd.ValueObjects;


namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseFeatureRolloutCountry : StringValueObject
    {
        protected ReleaseFeatureRolloutCountry(string value) : base(value)
        {
        }
    }
}
