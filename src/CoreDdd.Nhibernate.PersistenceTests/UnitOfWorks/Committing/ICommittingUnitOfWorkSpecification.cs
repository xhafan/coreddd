using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.PersistenceTests.UnitOfWorks.Committing
{
    public interface ICommittingUnitOfWorkSpecification
    {
        void CommitAct(IUnitOfWork unitOfWork);
    }
}