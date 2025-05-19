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
            var rebusTransactionScopeUnitOfWork = new RebusTransactionScopeUnitOfWork(unitOfWorkFactory: null!);
            var fakeMessageContext = new FakeMessageContext();

            var ex = Should.Throw<InvalidOperationException>(() => rebusTransactionScopeUnitOfWork.Create(fakeMessageContext));

            ex.Message.ShouldBe("UnitOfWork factory is not set.");
        }
    }
}
