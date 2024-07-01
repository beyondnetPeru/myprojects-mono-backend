namespace Ddd.ValueObjects
{
    public class DateTimeValueObject : ValueObject<DateTimeValueObject>
    {
        public DateTime Value { get; }

        protected DateTimeValueObject(DateTime value)
        {
            Value = value.ToUniversalTime();
        }

        public static DateTimeValueObject Create(DateTime value)
        {
            return new DateTimeValueObject(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
