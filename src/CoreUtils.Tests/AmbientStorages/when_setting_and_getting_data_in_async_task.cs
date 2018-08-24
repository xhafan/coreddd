#if !NET40
using System.Threading.Tasks;
using CoreUtils.AmbientStorages;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.AmbientStorages
{
    [TestFixture]
    public class when_setting_and_getting_data_in_async_task
    {
        [Test]
        public async Task data_flows_with_the_execution_context_into_async_task()
        {
            var ambientStorage = new AmbientStorage<int> {Value = 23};

            await Task.Run(() =>
            {
                ambientStorage.Value.ShouldBe(23);
            });
        }
    }
}
#endif