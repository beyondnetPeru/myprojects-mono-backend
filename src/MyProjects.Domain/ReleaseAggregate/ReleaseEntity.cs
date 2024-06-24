using Ddd;
using Ddd.Interfaces;
using Ddd.ValueObjects;
using MyProjects.Domain.ReleaseAggregate.Events;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class ReleaseEntity : Entity<ReleaseEntity>, IAggregateRoot
    {
        public StringValueObject Title { get; private set; }
        public StringValueObject Description { get; private set; }
        public DateTimeValueObject GoLiveDate { get; private set; }
        public StringValueObject? Owner { get; private set; }
        public List<ReleaseFeature>? Features { get; private set; }
        public List<ReleaseReference>? References { get; private set; }
        public List<ReleaseComment>? Comments { get; private set; }
        public ReleaseVersion VersionNumber { get; private set; } 

        public ReleaseStatus Status { get; set; }

        private ReleaseEntity(StringValueObject title, StringValueObject description)
        {
            Title = title;
            Description = description;
            GoLiveDate = DateTimeValueObject.Create(DateTime.Now);
            Owner = StringValueObject.Create(string.Empty);
            VersionNumber = ReleaseVersion.Create(StageEnum.Alpha,0,0,0);

            Features = new List<ReleaseFeature>();
            References = new List<ReleaseReference>();
            Comments = new List<ReleaseComment>();

            Status = ReleaseStatus.Created;

            AddDomainEvent(new ReleaseCreatedDomainEvent(title.GetValue(), description.GetValue()));
        }

        public static ReleaseEntity Create(StringValueObject name, StringValueObject description)
        {
            return new ReleaseEntity(name, description);
        }

        public void ChangeTitle(StringValueObject title)
        {
            if (Status != ReleaseStatus.Created)
            {
                AddBrokenRule("Title", "Title can be changed only if release is in Created status");
                return;
            }

            Title = title;

            SetDirty();
        }

        public void ChangeDescription(StringValueObject description)
        {
            if (Status != ReleaseStatus.Created)
            {
                AddBrokenRule("Description", "Description can be changed only if release is in Created status");
                return;
            }

            Description = description;

            SetDirty();
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

        public void Schedule(DateTimeValueObject goLiveDate)
        {
            if (Status != ReleaseStatus.Open)
            {
                AddBrokenRule("Status", "Release can be scheduled only if it is in Open status");
                return;
            }

            GoLiveDate = goLiveDate;
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

        public void OnHold()
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

            VersionNumber = version;

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
