using Ddd;

namespace MyProjects.Domain.ProjectAggregate
{
    public class GoLive : ValueObject<GoLive>
    {
        public DateTime Value { get; }

        protected GoLive(DateTime value)
        {
            Value = value;
        }

        public static GoLive Create(DateTime value)
        {
            return new GoLive(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
