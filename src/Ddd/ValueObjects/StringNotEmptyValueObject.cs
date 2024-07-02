
using Ddd.ValueObjects.Validators;

namespace Ddd.ValueObjects
{
    public class StringNotEmptyValueObject : ValueObject<string>
    {

        public StringNotEmptyValueObject(string value) : base(value)
        {
            AddBusinessRule(new StringNotEmptyValueObjectValidator());
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
