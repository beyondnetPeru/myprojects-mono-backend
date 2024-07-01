using Ddd.ValueObjects.Validators;

namespace Ddd.ValueObjects
{
    public class IdValueObject : ValueObject<IdValueObject>
    {
        public string Value { get; }

        private IdValueObject(string value) 
        {
           this.AddBusinessRule(new IdValueObjectCannotBeEmptyRule());
            Value = value;
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
            return new IdValueObject(Guid.NewGuid().ToString());
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}