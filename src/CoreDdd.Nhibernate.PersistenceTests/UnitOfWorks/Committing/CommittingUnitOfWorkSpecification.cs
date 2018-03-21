using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.PersistenceTests.UnitOfWorks.Committing
{
    public class CommittingUnitOfWorkSpecification : ICommittingUnitOfWorkSpecification
    {
        public void CommitAct(IUnitOfWork unitOfWork)
        {
            unitOfWork.Commit();
        }
    }
}