using Ddd.ValueObjects;
using FluentValidation.Results;
using FluentValidation;
using MediatR;
using Ddd.Interfaces;

namespace Ddd
{
    public class Entity<T> where T : class
    {
        #region Members         

        private List<AbstractValidator<T>>? _businessRules;

        private List<ValidationFailure> _brokenRules;

        private List<INotification> _domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        #endregion

        #region Properties

        public IdValueObject Id { get; private set; }
        public TrackingEntity Traking { get; private set; } = default;
        
        public bool IsValid
        {
            get
            {
                Validate();
                return _brokenRules.Count == 0;
            }
        }
            
        public int Version { get; private set; }

        #endregion

        #region Constructor

        protected Entity()
        {
            _businessRules = new List<AbstractValidator<T>>();
            _brokenRules = new List<ValidationFailure>();
            _domainEvents = new List<INotification>();

            Id = IdValueObject.Create();
            
            Version = 0;

            BusinessRules();

            SetNew();
        }

        #endregion

        #region Traking

        public void SetNew()
        {
            Traking = new TrackingEntity { New = true, Dirty = false };
        }

        public void SetDirty()
        {
            Traking = new TrackingEntity { New = false, Dirty = true };
        }

        public void SetVersion(int version)
        {
            if (version <= 0)
            {
                AddBrokenRule("Version", "Version is less or equal than 0");
                return;
            }

            Version = version;
        }

        #endregion

        #region Business Rules

        private void BusinessRules()
        {

        }

        public void AddBusinessRule(AbstractValidator<T> rule)
        {
            if (_businessRules == null)
            {
                AddBrokenRule("BusinessRules", "BusinessRules is null");
                return;
            }

            _businessRules?.Add(rule);
        }

        public void AddBusinessRules(IEnumerable<AbstractValidator<T>> rules)
        {
            if (_businessRules == null)
            {
                AddBrokenRule("BusinessRules", "BusinessRules is null");
                return;
            }

            if (rules.Count() == 0)
            {
                AddBrokenRule("BusinessRules", "BusinessRules is empty");
                return;
            }

            _businessRules?.AddRange(rules);
        }

        public void AddBrokenRule(string propertyName, string message)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                AddBrokenRule("PropertyName", "PropertyName is null or empty");
                return;
            }

            if (string.IsNullOrEmpty(message))
            {
                AddBrokenRule("Message", "Message is null or empty");
                return;
            }

            _brokenRules.Add(new ValidationFailure(propertyName, message));
        }

        private void Validate()
        {
            if (_businessRules == null) return;

            var entity = this as T;

            // Run validation and add errors from Entity 
            _businessRules.ForEach(rule =>
            {
                var result = rule.Validate(entity!);

                if (!result.IsValid)
                {
                    _brokenRules.AddRange(result.Errors);
                }
            });

            // Add errors from ValueObjects
            var properties = entity!.GetType().GetProperties().Where(
                 p => p.PropertyType.IsSubclassOf(typeof(ValueObject<>)));

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    var valueObject = property as ValueObject<T>;

                    if (!valueObject!.IsValid)
                    {
                        _brokenRules.AddRange(valueObject!.GetBrokenRules());
                    }
                }
            }
        }

        public IReadOnlyCollection<ValidationFailure> GetBrokenRules()
        {
            return _brokenRules.AsReadOnly();
        }

        #endregion

        #region DomainEvents                        

        public void AddDomainEvent(INotification eventItem)
        {
            if (eventItem == null)
            {
                AddBrokenRule("EventItem", "EventItem is null");
                return;
            }

            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            if (eventItem == null)
            {
                AddBrokenRule("EventItem", "EventItem is null");
                return;
            }

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
