using System.Threading;
using NUnit.Framework;
using Shouldly;

namespace Core.Commons.Tests.UnitOfWorkTests
{
    [TestFixture]
    public class when_getting_current_unit_of_work_for_different_threads : base_when_getting_current_unit_of_work
    {
        private UnitOfWork _threadOneCurrentUnitOfWork;
        private UnitOfWork _threadTwoCurrentUnitOfWork;

        protected override void Context()
        {
            var thread = new Thread(ThreadMethod);
            thread.Start();

            _threadOneCurrentUnitOfWork = UnitOfWork.Current;
            thread.Join();
        }

        [Test]
        public void unit_of_works_are_different()
        {
            _threadOneCurrentUnitOfWork.ShouldNotBe(_threadTwoCurrentUnitOfWork);
        }

        private void ThreadMethod()
        {
            _threadTwoCurrentUnitOfWork = UnitOfWork.Current;            
        }

    }

}
