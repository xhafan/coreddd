using CoreUtils.Storages;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.Storages
{
    [TestFixture]
    public class when_setting_and_getting_data_into_storage
    {
        [Test]
        public void set_data_can_be_retrieved()
        {
            var storage = new Storage<int>();

            storage.Set(23);

            storage.Get().ShouldBe(23);
        }
    }
}