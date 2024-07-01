
using Ddd;
using Ddd.ValueObjects;
using MyProjects.Domain.ProjectAggregate.Events;

namespace MyProjects.Domain.ProjectAggregate
{
    public class ProjectFeature :  Entity<ProjectFeature>
    {
        public IdValueObject ProjectId { get; set; }        
        public StringValueObject FeatureName { get; set; }
        public StringValueObject? FeatureDescription { get; set; }
        public List<ProjectFeaturePhase> Phases { get; set; }
        public List<ProjectFeatureComment> Comments { get; set; }
        public List<ProjectFeatureRollout> Rollouts { get; set; }
        public ProjectFeatureStatus FeatureStatus { get; set; }

        private ProjectFeature(IdValueObject projectId, StringValueObject featureName, StringValueObject featureDescription)
        {
            ProjectId = projectId;            
            FeatureName = featureName;
            FeatureDescription = featureDescription;
            FeatureStatus = ProjectFeatureStatus.Registered;

            Phases = new List<ProjectFeaturePhase>();
            Comments = new List<ProjectFeatureComment>();
            Rollouts = new List<ProjectFeatureRollout>();

            AddDomainEvent(new ProjectFeatureRegisteredDomainEvent(ProjectId.GetValue(), Id.GetValue(), featureName.Value));
        }

        public static ProjectFeature Create(IdValueObject ProjectId, StringValueObject featureName, StringValueObject featureDescription)
        {
            return new ProjectFeature(ProjectId, featureName, featureDescription);
        }

        public void UpdateName(StringValueObject featureName)
        {
            FeatureName = featureName;
        }

        public void UpdateDescription(StringValueObject featureDescription)
        {
            FeatureDescription = featureDescription;
        }

        public void AddPhase(ProjectFeaturePhase phase)
        {
            if (FeatureStatus != ProjectFeatureStatus.Registered)
            {
                AddBrokenRule("FeatureStatus", "Feature is not registered");
                return;
            }

            if (Phases.Any(p => p.Name.ToString()!.Equals(phase.Name.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                AddBrokenRule("PhaseName", "Phase name already exists");
                return;
            }

            Phases.Add(phase);

            SetDirty();
        }

        public void RemovePhase(ProjectFeaturePhase phase)
        {
            if (FeatureStatus != ProjectFeatureStatus.Registered)
            {
                AddBrokenRule("FeatureStatus", "Feature is not registered");
                return;
            }

            if (!Phases.Contains(phase))
            {
                AddBrokenRule("Phase", "Phase not found");
                return;
            }

            Phases.Remove(phase);

            SetDirty();
        }

        public void AddRollout(ProjectFeatureRollout rollout)
        {
            if (FeatureStatus != ProjectFeatureStatus.Registered)
            {
                AddBrokenRule("FeatureStatus", "Feature is not registered");
                return;
            }
            
            if(Rollouts.Contains(rollout))
            {
                AddBrokenRule("RolloutName", "Rollout Country already exists in the same date");
                return;
            }

            Rollouts.Add(rollout);

            SetDirty();
        }

        public void RemoveRollout(ProjectFeatureRollout rollout)
        {
            if (FeatureStatus != ProjectFeatureStatus.Registered)
            {
                AddBrokenRule("FeatureStatus", "Feature is not registered");
                return;
            }

            if (!Rollouts.Contains(rollout))
            {
                AddBrokenRule("Rollout", "Rollout not found");
                return;
            }

            Rollouts.Remove(rollout);

            SetDirty();
        }

        public void AddComment(ProjectFeatureComment comment)
        {

            if (FeatureStatus == ProjectFeatureStatus.Canceled)
            {
                AddBrokenRule("FeatureStatus", "Feature is canceled");
                return;
            }

            if (Comments.Contains(comment))
            {
                AddBrokenRule("Comment", "Comment already exists");
                return;
            }

            Comments.Add(comment);

            SetDirty();
        }

        public void RemoveComment(ProjectFeatureComment comment)
        {
            if (FeatureStatus == ProjectFeatureStatus.Canceled)
            {
                AddBrokenRule("FeatureStatus", "Feature is canceled");
                return;
            }

            if (!Comments.Contains(comment))
            {
                AddBrokenRule("Comment", "Comment not found");
                return;
            }

            Comments.Remove(comment);

            SetDirty();
        }

        public void OnHold()
        {
            if (FeatureStatus != ProjectFeatureStatus.Registered)
            {
                AddBrokenRule("FeatureStatus", "Feature is not registered");
                return;
            }

            FeatureStatus = ProjectFeatureStatus.OnHold;

            SetDirty();
        }

        public void Cancel()
        {
            if (FeatureStatus == ProjectFeatureStatus.Canceled)
            {
                AddBrokenRule("FeatureStatus", "Feature is already canceled");
                return;
            }

            FeatureStatus = ProjectFeatureStatus.Canceled;

            SetDirty();
        }

        public void Resume()
        {
            if (FeatureStatus != ProjectFeatureStatus.OnHold)
            {
                AddBrokenRule("FeatureStatus", "Feature is not on hold");
                return;
            }

            FeatureStatus = ProjectFeatureStatus.Registered;

            SetDirty();
        }
    }
}
