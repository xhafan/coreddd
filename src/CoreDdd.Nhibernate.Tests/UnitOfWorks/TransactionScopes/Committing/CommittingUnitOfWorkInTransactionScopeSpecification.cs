using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes.Committing
{
    public class CommittingUnitOfWorkInTransactionScopeSpecification : ICommittingUnitOfWorkInTransactionScopeSpecification
    {
        public void CommitAct(IUnitOfWork unitOfWork)
        {
            unitOfWork.Commit();
        }
    }
}