using System.Threading;
using NUnit.Framework;
using Shouldly;

namespace Core.Commons.Tests
{
    [TestFixture]
    public class when_getting_current_unit_of_work_for_different_threads : base_when_getting_current_unit_of_work
    {
        private IUnitOfWork _threadOneCurrentUnitOfWork;
        private IUnitOfWork _threadTwoCurrentUnitOfWork;

        protected override void Context()
        {
            var thread = new Thread(ThreadMethod);
            thread.Start();

            _threadOneCurrentUnitOfWork = UnitOfWork.Current;
            thread.Join();
        }

        [Test]
        public void unit_of_works_are_the_same()
        {
            _threadOneCurrentUnitOfWork.ShouldNotBe(_threadTwoCurrentUnitOfWork);
        }

        private void ThreadMethod()
        {
            _threadTwoCurrentUnitOfWork = UnitOfWork.Current;            
        }

    }

}
