using Ddd.ValueObjects;
using System.ComponentModel;

namespace Ddd.Test
{
    [TestClass]
    public class ValueObjectTest
    {
        [TestMethod]
        public void Should_Implement_INotifyPropertyChanged()
        {
            var owner = StringNotEmptyValueObject.Create("foo");

            Assert.IsInstanceOfType(owner, typeof(INotifyPropertyChanged));
        }

        [TestMethod]
        public void Should_Raise_PropertyChanged_Event()
        {
            var owner = StringNotEmptyValueObject.Create("foo");

            var eventWasRaised = false;
            owner.PropertyChanged += (sender, args) => eventWasRaised = true;

            owner.Value = "bar";

            Assert.IsTrue(eventWasRaised);
        }


        [TestMethod]
        public void Should_Not_Raise_PropertyChanged_Event_When_Property_Is_Not_Value()
        {
            var owner = StringNotEmptyValueObject.Create("foo");

            var eventWasRaised = false;
            owner.PropertyChanged += (sender, args) => eventWasRaised = true;

            owner.OnPropertyChanged("foo");

            Assert.IsFalse(eventWasRaised);
        }

    }
}
