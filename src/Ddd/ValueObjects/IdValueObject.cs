using Ddd.ValueObjects.Validators;

namespace Ddd.ValueObjects
{
    public class IdValueObject : ValueObject<IdValueObject>
    {
        public string Value { get; private set; }

        protected IdValueObject()
        {           
            Value = Guid.NewGuid().ToString();

            this.AddBusinessRule(new IdValueObjectValidator());

            this.Validate(this);
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

        public static IdValueObject Create()
        {
            return new IdValueObject();
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
