using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseVersionName : StringValueObject
    {

        private ReleaseVersionName(string value) : base(value)
        {
        }
    }
}
