using Ddd.ValueObjects;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseFeatureCommentAuthor : StringValueObject
    {
        protected ReleaseFeatureCommentAuthor(string value) : base(value)
        {
        }
    }
}
