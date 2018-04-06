using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes.Committing
{
    public interface ICommittingUnitOfWorkInTransactionScopeSpecification
    {
        void CommitAct(IUnitOfWork unitOfWork);
    }
}