using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseName : StringValueObject
    {
        protected ReleaseName(string value) : base(value)
        {
        }
    }
}
