using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ddd
{
    public abstract class ValueObject<T> : INotifyPropertyChanged where T : class
    {
        #region Members

        private T? _value;

        private ValidationResult brokenRules;

        private List<AbstractValidator<T>> businessRules;

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Properties

        public bool IsValid => brokenRules.Errors.Any();

        public T Value { 
            get => Value;
            set {
                _value = value;
                OnPropertyChanged(nameof(this.Value));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName.ToLower() != "value")
            {
                return;
            }

            Validate();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));          
        }

        #endregion

        #region Constructor

        protected ValueObject(T value)
        {
            _value = value;

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

        private void CheckBusinessRules()
        {
            var instance = this._value;

            foreach (var rule in businessRules)
            {
                var validation = rule.Validate(instance!);
                if (!validation.IsValid)
                {
                    brokenRules.Errors.AddRange(validation.Errors);
                }
            }
        }

        public virtual void Validate()
        {
            var PropertyValueFound = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(m => m.Name.ToLower() == "value");

            if (!PropertyValueFound.Any())
            {
                AddBrokenRule("ValueObject", "ValueObject has no properties");
            }

            CheckBusinessRules();
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
