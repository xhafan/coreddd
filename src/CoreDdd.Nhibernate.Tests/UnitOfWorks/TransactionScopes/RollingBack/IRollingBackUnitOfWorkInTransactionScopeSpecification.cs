using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes.RollingBack
{
    public interface IRollingBackUnitOfWorkInTransactionScopeSpecification
    {
        void RollbackAct(IUnitOfWork unitOfWork);
    }
}