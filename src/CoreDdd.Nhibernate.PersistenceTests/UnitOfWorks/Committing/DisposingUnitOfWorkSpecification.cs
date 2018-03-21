using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.PersistenceTests.UnitOfWorks.Committing
{
    public class DisposingUnitOfWorkSpecification : ICommittingUnitOfWorkSpecification
    {
        public void CommitAct(IUnitOfWork unitOfWork)
        {
            unitOfWork.Dispose();
        }
    }
}