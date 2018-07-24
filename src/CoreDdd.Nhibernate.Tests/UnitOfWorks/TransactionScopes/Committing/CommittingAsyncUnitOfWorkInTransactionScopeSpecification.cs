#if !NET40 && !NET45
using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes.Committing
{
    public class CommittingAsyncUnitOfWorkInTransactionScopeSpecification : ICommittingUnitOfWorkInTransactionScopeSpecification
    {
        public void CommitAct(IUnitOfWork unitOfWork)
        {
            unitOfWork.CommitAsync().Wait();
        }
    }
}
#endif