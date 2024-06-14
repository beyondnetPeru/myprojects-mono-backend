using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseOwner : StringValueObject
    {
        protected ReleaseOwner(string value) : base(value)
        {
        }
    }
}
