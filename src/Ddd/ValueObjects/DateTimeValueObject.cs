namespace Ddd.ValueObjects
{
    public class DateTimeValueObject : ValueObject<DateTime>
    {
        public DateTime Value { get; private set; }

        protected DateTimeValueObject()
        {
            Value = DateTime.Now;
        }

        public static DateTimeValueObject Create(DateTime value)
        {
            return new DateTimeValueObject
            {
                Value = value
            };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
