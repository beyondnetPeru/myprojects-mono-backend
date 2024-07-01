namespace Ddd.ValueObjects
{
    public class StringValueObject : ValueObject<StringValueObject>
    {
        public string Value { get; }

        protected StringValueObject(string value)
        {
            Value = value;
        }

        public static StringValueObject Create(string value)
        {
            return new StringValueObject(value);
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
