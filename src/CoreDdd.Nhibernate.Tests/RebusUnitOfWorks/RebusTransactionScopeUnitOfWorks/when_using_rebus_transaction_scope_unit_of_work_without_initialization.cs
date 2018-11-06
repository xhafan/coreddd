#if !NET40 && !NET45
using System;
using CoreDdd.Nhibernate.Tests.RebusUnitOfWorks.RebusTransactionScopeUnitOfWorks;
using CoreDdd.Rebus.UnitOfWork;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.RebusUnitOfWorks.RebusTransactionScopeUnitOfWorks
{
    [TestFixture]
    public class when_using_rebus_transaction_scope_unit_of_work_without_initialization
    {
        [Test]
        public void exception_is_thrown()
        {
            RebusTransactionScopeUnitOfWork.Initialize(unitOfWorkFactory: null);
            var fakeMessageContext = new FakeMessageContext();

            var ex = Should.Throw<InvalidOperationException>(() => RebusTransactionScopeUnitOfWork.Create(fakeMessageContext));

            ex.Message.ShouldBe(
                "RebusTransactionScopeUnitOfWork has not been initialized! Please call RebusTransactionScopeUnitOfWork.Initialize(...) before using it.");
        }
    }
}
#endif