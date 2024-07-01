using Ddd;
using Ddd.ValueObjects;

namespace MyProjects.Domain.ProjectAggregate
{
    public class ProjectReference : Entity<ProjectReference>
    {
        public IdValueObject ProjectId { get; set; }
        public StringValueObject ReferenceName { get; set; }
        public StringValueObject ReferenceUrl { get; set; }

        private ProjectReference(IdValueObject ProjectId, StringValueObject referenceName, StringValueObject referenceUrl)
        {
            ProjectId = ProjectId;
            ReferenceName = referenceName;
            ReferenceUrl = referenceUrl;
        }

        public static ProjectReference Create(IdValueObject ProjectId, StringValueObject referenceName, StringValueObject referenceUrl)
        {
            return new ProjectReference(ProjectId, referenceName, referenceUrl);
        }

    }
}
