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
    public class when_setting_and_getting_data_in_asp_net_with_http_context_current
    {
        [Test]
        public void data_are_correctly_retrieved_from_http_context()
        {
            var fakeHttpContext = FakeHttpContextHelper.GetFakeHttpContext();
            HttpContext.Current = fakeHttpContext;

            var ambientStorage = new AmbientStorage<int> {Value = 23};

            var threadValue = 0;
            var thread = new Thread(() =>
            {
                HttpContext.Current = fakeHttpContext;

                threadValue = ambientStorage.Value;
            });
            thread.Start();
            thread.Join();

            threadValue.ShouldBe(23);
        }

        [TearDown]
        public void TearDown()
        {
            HttpContext.Current = null;
        }
    }
}
#endif