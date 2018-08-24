using CoreUtils.AmbientStorages;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.AmbientStorages
{
    [TestFixture]
    public class when_setting_and_getting_data_in_multiple_storage_instances
    {
        [Test]
        public void data_are_correctly_retrieved()
        {
            var ambientStorageOne = new AmbientStorage<int> {Value = 23};
            var ambientStorageTwo = new AmbientStorage<int> {Value = 24};
            
            ambientStorageOne.Value.ShouldBe(23);
            ambientStorageTwo.Value.ShouldBe(24);
        }
    }
}