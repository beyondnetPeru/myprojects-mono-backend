
using Ddd.ValueObjects.Validators;

namespace Ddd.ValueObjects
{
    public class StringNotEmptyValueObject : ValueObject<StringNotEmptyValueObject>
    {
        public string Value { get; }

        protected StringNotEmptyValueObject(string value) 
        {
            Value = value;

            this.AddBusinessRule(new StringNotEmptyValueObjectValidator());

            this.Validate(this);
        }

        public static StringNotEmptyValueObject Create(string value)
        {
            return new StringNotEmptyValueObject(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
