using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseFeaturePhaseName : StringValueObject
    {
        protected ReleaseFeaturePhaseName(string value) : base(value)
        {
        }
    }
}
