using Ddd.ValueObjects;
using FluentValidation.Results;
using FluentValidation;
using MediatR;
using Ddd.Interfaces;

namespace Ddd
{
    public class Entity<T>
    {
        #region Members 
        int? _requestedHashCode;

        private List<AbstractValidator<T>>? _businessRules;

        private List<ValidationFailure> _brokenRules;

        #endregion

        #region Properties

        public IdValueObject Id { get; private set; }
        public AuditValueObject? Audit { get; set; }
        public TrackingEntity Traking { get; private set; } = default;

        #endregion

        #region Properties

        public bool IsValid => _brokenRules!.Any();

        #endregion

        #region Constructor

        protected Entity()
        {
            _businessRules = new List<AbstractValidator<T>>();
            _brokenRules = new List<ValidationFailure>();

            Id = IdValueObject.Create();

            SetNew();            
        }

        #endregion

        #region Traking

        public void SetNew()
        {
            Traking = new TrackingEntity { New = true, Dirty = false };

            // TODO: Get user by context
            Audit = AuditValueObject.Create("default");
        }

        public void SetDirty()
        {
            Traking = new TrackingEntity { New = false, Dirty = true };

            // TODO: Get user by context
            Audit = AuditValueObject.Create("default");
        }

        #endregion

        #region Business Rules
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
            _brokenRules.Add(new ValidationFailure(propertyName, message));
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
                    _brokenRules.AddRange(result.Errors);
                }
            }
        }

        public IReadOnlyCollection<ValidationFailure> GetBrokenRules()
        {
            return _brokenRules.AsReadOnly();
        }

        #endregion

        #region DomainEvents

        private List<INotification> _domainEvents = new List<INotification>();
        private TrackingEntity traking;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
        
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        #endregion

        #region Equality
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<T>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            Entity<T> item = (Entity<T>)obj;

            
            return item.Id == Id;
        }

        public override int GetHashCode()
        {
           return base.GetHashCode();
        }

        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right)
        {
            return !(left == right);
        }
        #endregion
    }
}
