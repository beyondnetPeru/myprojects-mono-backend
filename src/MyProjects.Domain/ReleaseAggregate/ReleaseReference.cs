using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseReference : StringValueObject
    {
        protected ReleaseReference(string value) : base(value)
        {
        }
    }
}
