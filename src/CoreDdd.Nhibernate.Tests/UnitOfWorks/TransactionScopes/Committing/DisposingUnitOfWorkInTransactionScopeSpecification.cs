using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes.Committing
{
    public class DisposingUnitOfWorkInTransactionScopeSpecification : ICommittingUnitOfWorkInTransactionScopeSpecification
    {
        public void CommitAct(IUnitOfWork unitOfWork)
        {
            unitOfWork.Dispose();
        }
    }
}