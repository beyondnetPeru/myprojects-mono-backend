using FluentValidation;
using FluentValidation.Results;

namespace Ddd
{
    public abstract class ValueObject<T> where T : class
    {
        #region Members

        private ValidationResult brokenRules;
        private List<AbstractValidator<T>> businessRules;

        #endregion

        #region Properties

        public bool IsValid { 
            get {
                return brokenRules.Errors.Any();
            }
        }

        #endregion

        #region Constructor

        protected ValueObject()
        {
            brokenRules = new ValidationResult();
            businessRules = new List<AbstractValidator<T>>();
        }

        #endregion

        #region BusinessRules

        public void AddBrokenRule(string propertyName, string message)
        {
            brokenRules.Errors.Add(new ValidationFailure(propertyName, message));
        }

        public void AddBusinessRule(AbstractValidator<T> rule)
        {
            businessRules.Add(rule);
        }

        public void AddBusinessRules(IEnumerable<AbstractValidator<T>> rules)
        {
            businessRules.AddRange(rules);
        }

        public void Validate(T instance)
        {
            foreach (var rule in businessRules)
            {
                var validation = rule.Validate(instance);
                if (!validation.IsValid)
                {
                    brokenRules.Errors.AddRange(validation.Errors);
                }
            }
        }

        #endregion

        #region Equals

        protected static bool EqualOperator(ValueObject<T> left, ValueObject<T> right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right!);
        }

        protected static bool NotEqualOperator(ValueObject<T> left, ValueObject<T> right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject<T>)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public ValueObject<T> GetCopy()
        {
            return (ValueObject<T>)MemberwiseClone();
        }

        #endregion
    }
}
