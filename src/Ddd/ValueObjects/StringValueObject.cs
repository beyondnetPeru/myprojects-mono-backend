using Ddd.ValueObjects.Validators;

namespace Ddd.ValueObjects
{
    public class StringValueObject : ValueObject<string>
    {
        public static string? Value { get; private set; }

        protected StringValueObject(string value)
        {
            Value = value;

            this.AddBusinessRule(new StringValueObjectValidator());

            Validate(this.GetValue());
        }

        public static StringValueObject Create(string value)
        {
            return new StringValueObject(value);
            
        }

        public void SetValue(string value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            Value = value;
        }

        public string GetValue()
        {
            return Value!;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value!;
        }
    }
}
