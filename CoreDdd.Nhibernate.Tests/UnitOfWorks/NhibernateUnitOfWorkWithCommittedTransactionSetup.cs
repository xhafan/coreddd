using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    public abstract class NhibernateUnitOfWorkWithCommittedTransactionSetup : NhibernateUnitOfWorkWithStartedTransactionSetup
    {
        protected ITransaction Transaction;

        [SetUp]
        public override void Context()
        {
            base.Context();

            Transaction = Mock<ITransaction>();
            Session.Stubs(x => x.Transaction).Returns(Transaction);

            UnitOfWork.Commit();
        }
    }
}