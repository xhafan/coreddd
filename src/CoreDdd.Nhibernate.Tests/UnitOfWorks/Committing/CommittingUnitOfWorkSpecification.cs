using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.Committing
{
    public class CommittingUnitOfWorkSpecification : ICommittingUnitOfWorkSpecification
    {
        public void CommitAct(IUnitOfWork unitOfWork)
        {
            unitOfWork.Commit();
        }
    }
}