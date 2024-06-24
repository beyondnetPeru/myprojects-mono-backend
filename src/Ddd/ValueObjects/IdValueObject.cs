using Ddd.ValueObjects.Validators;

namespace Ddd.ValueObjects
{
    public class IdValueObject : ValueObject<IdValueObject>
    {
        private string Value { get;  }

        protected IdValueObject()
        {           
            Value = Guid.NewGuid().ToString();

            this.AddBusinessRule(new IdValueObjectValidator());

            this.Validate(this);
        }

        protected IdValueObject(string value)
        {
            Value = value;

            this.AddBusinessRule(new IdValueObjectValidator());

            this.Validate(this);
        }

        public static IdValueObject SetValue(string value)
        {
            return new IdValueObject(value);
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
