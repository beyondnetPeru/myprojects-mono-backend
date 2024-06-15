using Ddd;
using Ddd.Interfaces;
using Ddd.ValueObjects;
using MyProjects.Domain.ReleaseAggregate.Events;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class Release : Entity<Release>, IAggregateRoot
    {
        public StringValueObject Name { get; set; }
        public StringValueObject Description { get; set; }
        public DateTimeValueObject GoLiveDate { get; set; }
        public StringValueObject? Owner { get; set; }
        public List<ReleaseFeature>? Features { get; set; }
        public List<ReleaseReference>? References { get; set; }
        public List<ReleaseComment>? Comments { get; set; }
        public ReleaseVersion Version { get; set; } 

        public ReleaseStatus Status { get; set; }

        private Release(StringValueObject name, StringValueObject description)
        {
            Name = name;
            Description = description;
            GoLiveDate = DateTimeValueObject.Create(DateTime.Now);
            Owner = StringValueObject.Create(string.Empty);
            Version = ReleaseVersion.Create(StageEnum.Alpha,0,0,0);

            Features = new List<ReleaseFeature>();
            References = new List<ReleaseReference>();
            Comments = new List<ReleaseComment>();

            Status = ReleaseStatus.Created;

            AddDomainEvent(new ReleaseCreatedDomainEvent(name.GetValue(), description.GetValue()));
        }

        public static Release Create(StringValueObject name, StringValueObject description)
        {
            return new Release(name, description);
        }

        public void Open()
        {
            if (Status != ReleaseStatus.Created)
            {
                AddBrokenRule("Status", "Release can be opened only if it is in Created status");
                return;
            }

            Status = ReleaseStatus.Open;

            // TODO: How fired an event to update dirty track status when a property change
            SetDirty();
        }

        public void Schedule()
        {
            if (Status != ReleaseStatus.Open)
            {
                AddBrokenRule("Status", "Release can be scheduled only if it is in Open status");
                return;
            }

            Status = ReleaseStatus.Scheduled;

            SetDirty();
        }

        public void Close()
        {
            if (Status != ReleaseStatus.Scheduled)
            {
                AddBrokenRule("Status", "Release can be closed only if it is in Scheduled status");
                return;
            }
                        
            Status = ReleaseStatus.Closed;

            SetDirty();
        }

        public void Hold()
        {
            if (Status != ReleaseStatus.Scheduled)
            {
                AddBrokenRule("Status", "Release can be put on hold only if it is in Scheduled status");
                return;
            }
           
            Status = ReleaseStatus.OnHold;

            SetDirty();
        }

        public void SetOwner(StringValueObject owner)
        {
           if (Status != ReleaseStatus.Created || Status != ReleaseStatus.Open)
           {
                AddBrokenRule("Owner", "Owner can be set only for Created or Open releases");
                return;
           }
                      
           Owner = owner;

            SetDirty();
        }

        public void SetVersion(ReleaseVersion version)
        {
            if (Status != ReleaseStatus.Open)
            {
                AddBrokenRule("Version", "Version can be set only for Open releases"  );
                return;
            }
                        
            Version = version;

            SetDirty();
        }


        public void SetGoLiveDate(DateTimeValueObject goLiveDate)
        {
            if (Status != ReleaseStatus.Scheduled)
            {
                AddBrokenRule("GoLiveDate", "GoLiveDate can be set only for Scheduled releases");
                return;
            }            

            GoLiveDate = goLiveDate;
            
            SetDirty();
        }

        public void ClearGoLiveDate()
        {
            if (Status != ReleaseStatus.Scheduled)
            {
                AddBrokenRule("GoLiveDate", "GoLiveDate can be cleared only for Scheduled releases");
                return;
            }            

            GoLiveDate = null;

            SetDirty();
        }
                

        public void AddFeature(ReleaseFeature feature)        
        {
            if (Status != ReleaseStatus.Open)
            {
                AddBrokenRule("Feature", "Feature can be added only for Open releases");
                return;
            }
            
            Features?.Add(feature);

            SetDirty();
        }

        public void RemoveFeature(ReleaseFeature feature)
        {
            if (Status != ReleaseStatus.Open)
            {
                AddBrokenRule("Feature", "Feature can be removed only for Open releases");
                return;
            }

            var exists = Features?.Any(f => f == feature);

            if (!exists.HasValue || !exists.Value)
            {
                AddBrokenRule("Feature", "Feature not found");
                return;
            }

            Features?.Remove(feature);

            SetDirty();
        }

        public void AddReference(ReleaseReference reference)
        {
            if (Status != ReleaseStatus.Open || Status != ReleaseStatus.Created || Status != ReleaseStatus.Scheduled) {
                AddBrokenRule("Reference", "Reference can be added only for Open, Created or Scheduled releases");
                return;
            }

            References?.Add(reference);

            SetDirty();
        }

        public void RemoveReference(ReleaseReference reference)
        {
            if (Status != ReleaseStatus.Open || Status != ReleaseStatus.Created || Status != ReleaseStatus.Scheduled)
            {
                AddBrokenRule("Reference", "Reference can be removed only for Open, Created or Scheduled releases");
                return;
            }

            var exists = References?.Any(r => r == reference);

            if (!exists.HasValue || !exists.Value)
            {
                AddBrokenRule("Reference", "Reference not found");
                return;
            }

            References?.Remove(reference);

            SetDirty();
        }

        public void AddComment(ReleaseComment comment)
        {
            if (Status != ReleaseStatus.Open || Status != ReleaseStatus.Created || Status != ReleaseStatus.Scheduled)
            {
                AddBrokenRule("Comment", "Comment can be added only for Open, Created or Scheduled releases");
                return;
            }

            Comments?.Add(comment);

            SetDirty();
        }

        public void RemoveComment(ReleaseComment comment)
        {
            if (Status != ReleaseStatus.Open || Status != ReleaseStatus.Created || Status != ReleaseStatus.Scheduled)
            {
                AddBrokenRule("Comment", "Comment can be removed only for Open, Created or Scheduled releases");
                return;
            }

            var exists = Comments?.Any(c => c == comment);

            if (!exists.HasValue || !exists.Value)
            {
                AddBrokenRule("Comment", "Comment not found");
                return;
            }

            Comments?.Remove(comment);

            SetDirty();
        }
    };
}
