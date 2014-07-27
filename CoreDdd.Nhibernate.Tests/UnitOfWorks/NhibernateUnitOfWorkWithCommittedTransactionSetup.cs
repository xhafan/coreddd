using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    public abstract class NhibernateUnitOfWorkWithCommittedTransactionSetup : NhibernateUnitOfWorkWithStartedTransactionSetup
    {
        [SetUp]
        public override void Context()
        {
            base.Context();

            UnitOfWork.Commit();
        }
    }
}