using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes
{
    public interface IRollingBackUnitOfWorkInTransactionScopeSpecificationxxxx
    {
        void RollbackAct(IUnitOfWork unitOfWork);
    }
}