#if !NET40 && !NET45 && !NET451
using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.Committing
{
    public class CommittingAsyncUnitOfWorkSpecification : ICommittingUnitOfWorkSpecification
    {
        public void CommitAct(IUnitOfWork unitOfWork)
        {
            unitOfWork.CommitAsync().Wait();
        }
    }
}
#endif