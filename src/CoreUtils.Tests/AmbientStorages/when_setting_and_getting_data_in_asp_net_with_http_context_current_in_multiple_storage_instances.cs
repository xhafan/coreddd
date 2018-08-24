#if NETFRAMEWORK
using System.Threading;
using System.Web;
using CoreDdd.TestHelpers.HttpContexts;
using CoreUtils.AmbientStorages;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.AmbientStorages
{
    [TestFixture]
    public class when_setting_and_getting_data_in_asp_net_with_http_context_current_in_multiple_storage_instances
    {
        [Test]
        public void data_are_correctly_retrieved_from_http_context_from_multiple_storage_instances()
        {
            var fakeHttpContext = FakeHttpContextHelper.GetFakeHttpContext();
            HttpContext.Current = fakeHttpContext;

            var ambientStorageOne = new AmbientStorage<int> {Value = 23};
            var ambientStorageTwo = new AmbientStorage<int> {Value = 24};

            var threadAmbientStorageOneValue = 0;
            var threadAmbientStorageTwoValue = 0;
            var thread = new Thread(() =>
            {
                HttpContext.Current = fakeHttpContext;

                threadAmbientStorageOneValue = ambientStorageOne.Value;
                threadAmbientStorageTwoValue = ambientStorageTwo.Value;
            });
            thread.Start();
            thread.Join();

            threadAmbientStorageOneValue.ShouldBe(23);
            threadAmbientStorageTwoValue.ShouldBe(24);
        }

        [TearDown]
        public void TearDown()
        {
            HttpContext.Current = null;
        }
    }
}
#endif