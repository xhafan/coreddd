#if !NET40 && !NET45
using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes.RollingBack
{
    public class RollingBackAsyncUnitOfWorkInTransactionScopeSpecification : IRollingBackUnitOfWorkInTransactionScopeSpecification
    {
        public void RollbackAct(IUnitOfWork unitOfWork)
        {
            unitOfWork.RollbackAsync().Wait();
        }
    }
}
#endif