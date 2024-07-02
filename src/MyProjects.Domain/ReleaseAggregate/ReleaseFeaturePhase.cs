
using Ddd;
using Ddd.ValueObjects;
using MyProjects.Domain.ReleaseAggregate;

namespace MyReleases.Domain.ReleaseAggregate
{
    public class ReleaseFeaturePhase : Entity<ReleaseFeaturePhase>
    {
        public StringValueObject Name { get; set; } 
        public DateTimeValueObject StartDate { get; set; }
        public DateTimeValueObject EndDate { get; set; }
        public ReleaseFeaturePhaseStatus Status { get; set; }

        private ReleaseFeaturePhase(StringValueObject name)
        {
            Name = name;
            StartDate = DateTimeValueObject.Create(DateTime.Now);
            EndDate = DateTimeValueObject.Create(DateTime.Now.AddMonths(1));
            Status = ReleaseFeaturePhaseStatus.Registered;
        }

        public static ReleaseFeaturePhase Create(StringValueObject name)
        {
            return new ReleaseFeaturePhase(name);
        }
        
        public void UpdateName(StringValueObject name)
        {
            Name = name;

            SetDirty();
        }

        public void UpdateStartDate(DateTimeValueObject startDate)
        {
            if (startDate.Value.Date >= EndDate.Value.Date)
            {
                AddBrokenRule("StartDate", "Start date must be before end date");
                return;
            }

            StartDate = startDate;

            SetDirty();
        }

        public void UpdateEndDate(DateTimeValueObject endDate)
        {
            if (endDate.Value.Date <= StartDate.Value.Date)
            {
                AddBrokenRule("EndDate", "End date must be after start date");
                return;
            }

            EndDate = endDate;

            SetDirty();
        }

        public void OnHold()
        {
            if (Status == ReleaseFeaturePhaseStatus.Canceled)
            {
                AddBrokenRule("Status", "Phase is canceled");
                return;
            }

            Status = ReleaseFeaturePhaseStatus.OnHold;

            SetDirty();
        }

        public void Cancel()
        {
            if (Status == ReleaseFeaturePhaseStatus.Canceled)
            {
                AddBrokenRule("Status", "Phase is already canceled");
                return;
            }
            
            Status = ReleaseFeaturePhaseStatus.Canceled;

            SetDirty();
        }

        public void Resume()
        {
            if (Status != ReleaseFeaturePhaseStatus.OnHold)
            {
                AddBrokenRule("Status", "Phase is not on hold");
                return;
            }

            Status = ReleaseFeaturePhaseStatus.Registered;

            SetDirty();
        }
    }
}
