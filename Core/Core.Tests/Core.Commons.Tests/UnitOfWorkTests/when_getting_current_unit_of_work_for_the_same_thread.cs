using NUnit.Framework;
using Shouldly;

namespace Core.Commons.Tests.UnitOfWorkTests
{
    [TestFixture]
    public class when_getting_current_unit_of_work_for_the_same_thread : base_when_getting_current_unit_of_work
    {
        private UnitOfWork _currentUnitOfWork;
        private UnitOfWork _anotherCurrentUnitOfWork;

        protected override void Context()
        {
            _currentUnitOfWork = UnitOfWork.Current;
            _anotherCurrentUnitOfWork = UnitOfWork.Current;
        }

        [Test]
        public void unit_of_works_are_the_same()
        {
            _currentUnitOfWork.ShouldBe(_anotherCurrentUnitOfWork);
        }
    }
}