using Ddd;
using Ddd.Interfaces;
using Ddd.ValueObjects;
using MyProjects.Domain.ReleaseAggregate.Events;

namespace MyProjects.Domain.ReleaseAggregate
{
    public class Release : Entity<Release>, IAggregateRoot
    {
        public ReleaseName Name { get; set; }
        public ReleaseDescription Description { get; set; }
        public ReleaseGoLiveDate? GoLiveDate { get; set; }
        public ReleaseOwner? Owner { get; set; }
        public List<ReleaseFeature>? Features { get; set; }
        public List<ReleaseReference>? References { get; set; }
        public List<ReleaseCommernt>? Comments { get; set; }
        public ReleaseVersion? Version { get; private set; }
        public ReleaseVersionName? VersionName { get; set; }
        public ReleaseStatus Status { get; set; }

        private Release(ReleaseName name, ReleaseDescription description)
        {
            Name = name;
            Description = description;
            Features = new List<ReleaseFeature>();
            References = new List<ReleaseReference>();
            Comments = new List<ReleaseCommernt>();
            Status = ReleaseStatus.Created;

            AddDomainEvent(new ReleaseCreatedDomainEvent(name.GetValue(), description.GetValue()));
        }

        public static Release Create(ReleaseName name, ReleaseDescription description)
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

        public void SetOwner(ReleaseOwner owner)
        {
           if (Status != ReleaseStatus.Created || Status != ReleaseStatus.Open)
           {
                AddBrokenRule("Owner", "Owner can be set only for Created or Open releases");
                return;
           }
                      
           Owner = owner;

            SetDirty();
        }

        public void SetVersion(ReleaseVersion version, ReleaseVersionName versionName)
        {
            if (Status != ReleaseStatus.Open)
            {
                AddBrokenRule("Version", "Version can be set only for Open releases"  );
                return;
            }
                        
            Version = version;
            VersionName = versionName;

            SetDirty();
        }

        public void ClearVersion()
        {
            if (Status != ReleaseStatus.Open)
            {
                AddBrokenRule("Version", "Version can be cleared only for Open releases");
                return;
            }
            
            Version = null;
            VersionName = null;

            SetDirty();
        }

        public void SetGoLiveDate(ReleaseGoLiveDate goLiveDate)
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

        public void AddComment(ReleaseCommernt comment)
        {
            if (Status != ReleaseStatus.Open || Status != ReleaseStatus.Created || Status != ReleaseStatus.Scheduled)
            {
                AddBrokenRule("Comment", "Comment can be added only for Open, Created or Scheduled releases");
                return;
            }

            Comments?.Add(comment);

            SetDirty();
        }

        public void RemoveComment(ReleaseCommernt comment)
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
