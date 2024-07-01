using MediatR;
using FluentValidation.Results;
using FluentValidation;
using Ddd.Interfaces;
using Ddd.ValueObjects;

namespace Ddd
{
    public abstract class Entity<T> where T : class
    {
        #region Members         

        private List<INotification> _domainEvents;
        
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        private ValidationResult brokenRules;

        private List<AbstractValidator<T>> businessRules;

        #endregion

        #region Properties

        public IdValueObject Id { get; private set; }
        public TrackingEntity Traking { get; private set; } = default;
        
        public bool IsValid
        {
            get
            {
                return brokenRules.Errors.Any();
            }
        }
            
        public int Version { get; private set; }

        #endregion

        #region Constructor

        protected Entity()
        {

            _domainEvents = new List<INotification>();

            brokenRules = new ValidationResult();

            businessRules = new List<AbstractValidator<T>>();

            Id = IdValueObject.Create();
            
            Version = 0;

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
                return;
            }

            Version = version;
        }

        #endregion

        #region Business Rules

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

        public IReadOnlyCollection<ValidationFailure> GetBrokenRules()
        {
            return brokenRules.Errors;
        }

        #endregion

        #region DomainEvents                        

        public void AddDomainEvent(INotification eventItem)
        {
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
