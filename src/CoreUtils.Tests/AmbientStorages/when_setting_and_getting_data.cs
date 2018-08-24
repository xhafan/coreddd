using CoreUtils.AmbientStorages;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.AmbientStorages
{
    [TestFixture]
    public class when_setting_and_getting_data
    {
        [Test]
        public void data_are_correctly_retrieved()
        {
            var ambientStorage = new AmbientStorage<int> {Value = 23};
            
            ambientStorage.Value.ShouldBe(23);
        }
    }
}