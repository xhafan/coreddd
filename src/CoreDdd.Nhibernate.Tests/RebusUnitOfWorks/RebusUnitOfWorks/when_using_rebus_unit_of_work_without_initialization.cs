#if !NET40 && !NET45
using System;
using CoreDdd.Nhibernate.Tests.RebusUnitOfWorks.RebusTransactionScopeUnitOfWorks;
using CoreDdd.Rebus.UnitOfWork;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.RebusUnitOfWorks.RebusUnitOfWorks
{
    [TestFixture]
    public class when_using_rebus_unit_of_work_without_initialization
    {
        [Test]
        public void exception_is_thrown()
        {
            RebusUnitOfWork.Initialize(unitOfWorkFactory: null);
            var fakeMessageContext = new FakeMessageContext();

            var ex = Should.Throw<InvalidOperationException>(() => RebusUnitOfWork.Create(fakeMessageContext));

            ex.Message.ShouldBe("RebusUnitOfWork has not been initialized! Please call RebusUnitOfWork.Initialize(...) before using it.");
        }
    }
}
#endif