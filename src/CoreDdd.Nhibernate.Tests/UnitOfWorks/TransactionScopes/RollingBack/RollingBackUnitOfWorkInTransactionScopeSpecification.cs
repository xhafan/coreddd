using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes.RollingBack
{
    public class RollingBackUnitOfWorkInTransactionScopeSpecification : IRollingBackUnitOfWorkInTransactionScopeSpecification
    {
        public void RollbackAct(IUnitOfWork unitOfWork)
        {
            unitOfWork.Rollback();
        }
    }
}