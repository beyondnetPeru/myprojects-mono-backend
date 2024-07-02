using Ddd.ValueObjects;

namespace MyReleases.Domain.Shared
{
    public class Owner : StringValueObject
    {
        protected Owner(string value) : base(value)
        {
        }

        public static Owner Create(string value)
        {
            return new Owner(value);
        }

        public static Owner CreateDefault()
        {
            return new Owner("Default");
        }
    }
}
