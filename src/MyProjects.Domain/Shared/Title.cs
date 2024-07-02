using Ddd.ValueObjects;

namespace MyReleases.Domain.Shared
{
    public class Title : StringNotEmptyValueObject
    {
        public Title(string value) : base(value)
        {
            
        }
    }
}
