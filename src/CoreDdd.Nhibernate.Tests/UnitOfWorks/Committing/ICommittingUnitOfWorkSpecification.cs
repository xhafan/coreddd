using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.Committing
{
    public interface ICommittingUnitOfWorkSpecification
    {
        void CommitAct(IUnitOfWork unitOfWork);
    }
}