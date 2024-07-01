using Ddd.ValueObjects;

namespace MyProjects.Domain.Shared
{
    public class Title : StringNotEmptyValueObject
    {
        public Title(string value) : base(value)
        {
            
        }
    }
}
