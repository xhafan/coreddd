using System;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Rebus.UnitOfWork.Tests.RebusTransactionScopeUnitOfWorks
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
