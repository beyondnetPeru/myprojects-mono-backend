using Ddd.ValueObjects.Validators;

namespace Ddd.ValueObjects
{
    public class StringEmptyValueObject : ValueObject<string>
    {
        public static string Value { get; private set; }

        protected StringEmptyValueObject()
        {
            Value = string.Empty;

            this.AddBusinessRule(new StringValueObjectValidator());
        }

        public static void Create(string value)
        {
            Value = value;
        }

        public void SetValue(string value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            Value = value;
        }

        public string GetValue()
        {
            return Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value!;
        }
    }
}
