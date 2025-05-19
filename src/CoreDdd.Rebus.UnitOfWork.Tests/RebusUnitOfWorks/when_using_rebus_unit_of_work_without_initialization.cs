using System;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Rebus.UnitOfWork.Tests.RebusUnitOfWorks
{
    [TestFixture]
    public class when_using_rebus_unit_of_work_without_initialization
    {
        [Test]
        public void exception_is_thrown()
        {
            var rebusUnitOfWork = new RebusUnitOfWork(unitOfWorkFactory: null!);
            var fakeMessageContext = new FakeMessageContext();

            var ex = Should.Throw<InvalidOperationException>(() => rebusUnitOfWork.Create(fakeMessageContext));

            ex.Message.ShouldBe("UnitOfWork factory is not set.");
        }
    }
}
