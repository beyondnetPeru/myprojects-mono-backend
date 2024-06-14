using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseVersion : StringValueObject
    {
        protected ReleaseVersion(string value) : base(value)
        {
        }
    }
}
