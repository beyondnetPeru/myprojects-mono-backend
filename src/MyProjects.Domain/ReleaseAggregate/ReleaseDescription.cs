using Ddd;
using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseDescription : StringValueObject
    {
        protected ReleaseDescription(string value) : base(value)
        {
        }
    }
}
