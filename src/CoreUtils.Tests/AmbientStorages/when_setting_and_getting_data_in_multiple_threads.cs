using System.Threading;
using CoreUtils.AmbientStorages;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.AmbientStorages
{
    [TestFixture]
    public class when_setting_and_getting_data_in_multiple_threads
    {
        [Test]
        public void data_are_correctly_set_and_retrieved_by_multiple_threads()
        {
            var ambientStorage = new AmbientStorage<int>();
            var threadOneValueSet = false;
            var threadTwoValueSet = false;
            var threadOneValue = 0;
            var threadTwoValue = 0;

            var threadOne = new Thread(() =>
            {
                ambientStorage.Value = 23;
                threadOneValueSet = true;
                while (!threadTwoValueSet) Thread.Sleep(0);

                threadOneValue = ambientStorage.Value;
            });
            var threadTwo = new Thread(() =>
            {
                ambientStorage.Value = 24;
                threadTwoValueSet = true;
                while (!threadOneValueSet) Thread.Sleep(0);

                threadTwoValue = ambientStorage.Value;
            });

            threadOne.Start();
            threadTwo.Start();

            threadOne.Join();
            threadTwo.Join();

            threadOneValue.ShouldBe(23);
            threadTwoValue.ShouldBe(24);
        }
    }
}