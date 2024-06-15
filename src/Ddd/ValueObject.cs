using FluentValidation;
using FluentValidation.Results;


namespace Ddd
{
    public abstract class ValueObject<T>
    {
        #region Members

        private List<AbstractValidator<T>>? _businessRules;
        private List<ValidationFailure>? _brokenRules;

        #endregion

        #region Properties

        public bool IsValid => _brokenRules!.Any();

        #endregion

        #region Constructor
        
        public ValueObject()
        {
            _businessRules = new List<AbstractValidator<T>>();
            _brokenRules = new List<ValidationFailure>();

            Guard();
        }

        #endregion

        #region Business Rules
        private void Guard()
        {
            var properties = this.GetType().GetProperties().Where(p =>
                p.Name != "IsValid");

            if (properties == null)
            {
                AddBrokenRule("ValueObject", "ValueObject must have at least one property to be validated.");
                return;
            }

        }
        public void AddBusinessRule(AbstractValidator<T> rule)
        {
            ArgumentNullException.ThrowIfNull(rule, nameof(rule));

            _businessRules?.Add(rule);
        }

        public void AddBusinessRules(IEnumerable<AbstractValidator<T>> rules)
        {
            ArgumentNullException.ThrowIfNull(rules, nameof(rules));

            _businessRules?.AddRange(rules);
        }

        public void AddBrokenRule(string propertyName, string message)
        {
            _brokenRules?.Add(new ValidationFailure(propertyName, message));
        }

        public void Validate(T item)
        {
            ValidationResult result;

            if (_businessRules == null)
            {
                return;
            }

            foreach (var rule in _businessRules)
            {
                result = rule.Validate(item);

                if (!result.IsValid)
                {
                    _brokenRules?.AddRange(result.Errors);
                }
            }
        }

        public IReadOnlyCollection<ValidationFailure> GetBrokenRules()
        {
            return _brokenRules!.AsReadOnly();
        }

        #endregion

        #region Equality

        protected static bool EqualOperator(ValueObject<T> left, ValueObject<T> right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
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

        public ValueObject<T>? GetCopy()
        {
            return MemberwiseClone() as ValueObject<T>;
        }

        #endregion
    }
}
