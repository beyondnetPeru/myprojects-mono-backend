using Ddd;
using Ddd.Interfaces;
using Ddd.ValueObjects;
using MyProjects.Domain.ProjectAggregate.Events;
using MyProjects.Domain.Shared;

namespace MyProjects.Domain.ProjectAggregate
{
    public enum ProjectRiskLevel
    {
        HighRisk,
        MediumRisk,
        LowRisk
    }

    public class ProjectEntity : Entity<ProjectEntity>, IAggregateRoot
    {
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public GoLive GoLiveDate { get; private set; }
        public StringValueObject? Owner { get; private set; }
        public List<ProjectFeature>? Features { get; private set; }
        public List<ProjectReference>? References { get; private set; }
        public List<ProjectComment>? Comments { get; private set; }
        public ProjectVersion VersionNumber { get; private set; }
        public ProjectRiskLevel RiskLevel { get; private set; }
        public ProjectStatus Status { get; set; }

        private ProjectEntity(Title title, Description description)
        {
            Title = title;
            Description = description;
            GoLiveDate = GoLive.Create(DateTime.Now);
            Owner = StringValueObject.Create(string.Empty);
            VersionNumber = ProjectVersion.Create(StageEnum.Alpha,0,0,0);

            Features = new List<ProjectFeature>();
            References = new List<ProjectReference>();
            Comments = new List<ProjectComment>();

            Status = ProjectStatus.Created;

            AddDomainEvent(new ProjectCreatedDomainEvent(title.Value, description.Value));
        }

        public static ProjectEntity Create(Title title, Description description)
        {
            return new ProjectEntity(title, description);
        }

        public void ChangeTitle(Title title)
        {
            if (Status != ProjectStatus.Created)
            {
                AddBrokenRule("Title", "Title can be changed only if Project is in Created status");
                return;
            }

            Title = title;

            SetDirty();
        }

        public void ChangeDescription(Description description)
        {
            if (Status != ProjectStatus.Created)
            {
                AddBrokenRule("Description", "Description can be changed only if Project is in Created status");
                return;
            }

            Description = description;

            SetDirty();
        }

        public void Open()
        {
            if (Status != ProjectStatus.Created)
            {
                AddBrokenRule("Status", "Project can be opened only if it is in Created status");
                return;
            }

            Status = ProjectStatus.Open;

            // TODO: How fired an event to update dirty track status when a property change
            SetDirty();
        }

        public void Schedule(GoLive goLiveDate)
        {
            if (Status != ProjectStatus.Open)
            {
                AddBrokenRule("Status", "Project can be scheduled only if it is in Open status");
                return;
            }

            GoLiveDate = goLiveDate;
            Status = ProjectStatus.Scheduled;

            SetDirty();
        }

        public void Close()
        {
            if (Status != ProjectStatus.Scheduled)
            {
                AddBrokenRule("Status", "Project can be closed only if it is in Scheduled status");
                return;
            }
                        
            Status = ProjectStatus.Closed;

            SetDirty();
        }

        public void OnHold()
        {
            if (Status != ProjectStatus.Scheduled)
            {
                AddBrokenRule("Status", "Project can be put on hold only if it is in Scheduled status");
                return;
            }
           
            Status = ProjectStatus.OnHold;

            SetDirty();
        }

        public void SetOwner(StringValueObject owner)
        {
           if (Status != ProjectStatus.Created || Status != ProjectStatus.Open)
           {
                AddBrokenRule("Owner", "Owner can be set only for Created or Open Projects");
                return;
           }
                      
           Owner = owner;

            SetDirty();
        }

        public void SetVersion(ProjectVersion version)
        {
            if (Status != ProjectStatus.Open)
            {
                AddBrokenRule("Version", "Version can be set only for Open Projects"  );
                return;
            }

            VersionNumber = version;

            SetDirty();
        }


        public void SetGoLiveDate(GoLive goLiveDate)
        {
            if (Status != ProjectStatus.Scheduled)
            {
                AddBrokenRule("GoLiveDate", "GoLiveDate can be set only for Scheduled Projects");
                return;
            }            

            GoLiveDate = goLiveDate;
            
            SetDirty();
        }

        public void ClearGoLiveDate()
        {
            if (Status != ProjectStatus.Scheduled)
            {
                AddBrokenRule("GoLiveDate", "GoLiveDate can be cleared only for Scheduled Projects");
                return;
            }            

            GoLiveDate = null;

            SetDirty();
        }
                

        public void AddFeature(ProjectFeature feature)        
        {
            if (Status != ProjectStatus.Open)
            {
                AddBrokenRule("Feature", "Feature can be added only for Open Projects");
                return;
            }
            
            Features?.Add(feature);

            SetDirty();
        }

        public void RemoveFeature(ProjectFeature feature)
        {
            if (Status != ProjectStatus.Open)
            {
                AddBrokenRule("Feature", "Feature can be removed only for Open Projects");
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

        public void AddReference(ProjectReference reference)
        {
            if (Status != ProjectStatus.Open || Status != ProjectStatus.Created || Status != ProjectStatus.Scheduled) {
                AddBrokenRule("Reference", "Reference can be added only for Open, Created or Scheduled Projects");
                return;
            }

            References?.Add(reference);

            SetDirty();
        }

        public void RemoveReference(ProjectReference reference)
        {
            if (Status != ProjectStatus.Open || Status != ProjectStatus.Created || Status != ProjectStatus.Scheduled)
            {
                AddBrokenRule("Reference", "Reference can be removed only for Open, Created or Scheduled Projects");
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

        public void AddComment(ProjectComment comment)
        {
            if (Status != ProjectStatus.Open || Status != ProjectStatus.Created || Status != ProjectStatus.Scheduled)
            {
                AddBrokenRule("Comment", "Comment can be added only for Open, Created or Scheduled Projects");
                return;
            }

            Comments?.Add(comment);

            SetDirty();
        }

        public void RemoveComment(ProjectComment comment)
        {
            if (Status != ProjectStatus.Open || Status != ProjectStatus.Created || Status != ProjectStatus.Scheduled)
            {
                AddBrokenRule("Comment", "Comment can be removed only for Open, Created or Scheduled Projects");
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
