using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseFeatureCommentName : StringValueObject
    {
        protected ReleaseFeatureCommentName(string value) : base(value)
        {
        }
    }
}
