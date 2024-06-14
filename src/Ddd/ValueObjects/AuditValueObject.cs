

using System.Runtime.CompilerServices;

namespace Ddd.ValueObjects
{
    public class AuditValueObject : ValueObject<AuditValueObject>
    {
        public string CreatedBy { get; private set; } =  string.Empty;
        public DateTime CreatedAt { get; private set; }
        public string? UpdatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public TimeSpan TimeSpan { get; private set; }

        private AuditValueObject(string createdBy)
        {

            if (string.IsNullOrEmpty(createdBy))
            {
                AddBrokenRule("CreatedBy", "CreatedBy is required");
                return;
            }

            CreatedBy = createdBy;
            CreatedAt = DateTime.Now;
            UpdatedBy = string.Empty;
            UpdatedAt = null;
            TimeSpan = DateTime.Now.TimeOfDay;
        }

        public static AuditValueObject Create(string createdBy)
        {
            return new AuditValueObject(createdBy);            
        }

        public void Update(string updatedBy)
        {
            if (string.IsNullOrEmpty(updatedBy))
            {
                AddBrokenRule("UpdatedBy", "UpdatedBy is required");
                return;
            }

            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.Now;
            TimeSpan = DateTime.Now.TimeOfDay;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return
                CreatedBy +
                CreatedAt +
                UpdatedBy +
                UpdatedAt +
                TimeSpan;
        }
    }
}
