namespace Ddd.ValueObjects
{
    public class StringValueObject : ValueObject<string>
    {
        public StringValueObject(string value) : base(value)
        {
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
