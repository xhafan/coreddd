using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.Committing
{
    public class DisposingUnitOfWorkSpecification : ICommittingUnitOfWorkSpecification
    {
        public void CommitAct(IUnitOfWork unitOfWork)
        {
            unitOfWork.Dispose();
        }
    }
}