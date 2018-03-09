using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    public class NhibernateUnitOfWorkWithStartedTransactionSetup : NhibernateUnitOfWorkSetup
    {
        [SetUp]
        public override void Context()
        {
            base.Context();

            UnitOfWork.BeginTransaction();
        }
    }
}