using Ddd;

namespace MyReleases.Domain.ReleaseAggregate
{
    public enum StageEnum
    {
        Alpha = 1,
        Beta = 2,
        ReleaseCandidate = 3,
        Release = 4,
        GeneralAvailability = 5,
        PostRelease = 6
    }

    public class ReleaseVersion : ValueObject<ReleaseVersion>
    {
        public StageEnum Stage { get; } 
        public int Major { get; }
        public int Minor { get; }
        public int Patch { get; }

        private ReleaseVersion(StageEnum stage, int major, int minor, int patch)
        {
            Stage = stage;
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public static ReleaseVersion Create(StageEnum stage, int major, int minor, int patch)
        {
            return new ReleaseVersion(stage, major, minor, patch);
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
