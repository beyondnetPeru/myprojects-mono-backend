
using Ddd;
using Ddd.ValueObjects;

namespace MyProjects.Domain.ProjectAggregate
{
    public class ProjectFeaturePhase : Entity<ProjectFeaturePhase>
    {
        public StringValueObject Name { get; set; } 
        public DateTimeValueObject StartDate { get; set; }
        public DateTimeValueObject EndDate { get; set; }
        public ProjectFeaturePhaseStatus Status { get; set; }

        private ProjectFeaturePhase(StringValueObject name)
        {
            Name = name;
            StartDate = DateTimeValueObject.Create(DateTime.Now);
            EndDate = DateTimeValueObject.Create(DateTime.Now.AddMonths(1));
            Status = ProjectFeaturePhaseStatus.Registered;
        }

        public static ProjectFeaturePhase Create(StringValueObject name)
        {
            return new ProjectFeaturePhase(name);
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
            if (Status == ProjectFeaturePhaseStatus.Canceled)
            {
                AddBrokenRule("Status", "Phase is canceled");
                return;
            }

            Status = ProjectFeaturePhaseStatus.OnHold;

            SetDirty();
        }

        public void Cancel()
        {
            if (Status == ProjectFeaturePhaseStatus.Canceled)
            {
                AddBrokenRule("Status", "Phase is already canceled");
                return;
            }
            
            Status = ProjectFeaturePhaseStatus.Canceled;

            SetDirty();
        }

        public void Resume()
        {
            if (Status != ProjectFeaturePhaseStatus.OnHold)
            {
                AddBrokenRule("Status", "Phase is not on hold");
                return;
            }

            Status = ProjectFeaturePhaseStatus.Registered;

            SetDirty();
        }
    }
}
