using Ddd;

namespace MyProjects.Domain.ProjectAggregate
{
    public enum StageEnum
    {
        Alpha = 1,
        Beta = 2,
        ProjectCandidate = 3,
        Project = 4,
        GeneralAvailability = 5,
        PostProject = 6
    }

    public class ProjectVersion : ValueObject<ProjectVersion>
    {
        public StageEnum Stage { get; } 
        public int Major { get; }
        public int Minor { get; }
        public int Patch { get; }

        private ProjectVersion(StageEnum stage, int major, int minor, int patch)
        {
            Stage = stage;
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public static ProjectVersion Create(StageEnum stage, int major, int minor, int patch)
        {
            return new ProjectVersion(stage, major, minor, patch);
        }

        public string GetFullVersion()
        {
            return $"{Stage} {Major}.{Minor}.{Patch}";
        }

        public string GetVersion()
        {
            return $"{Major}.{Minor}.{Patch}";
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
