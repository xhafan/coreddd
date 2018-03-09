using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;

namespace CoreDdd.Nhibernate.Repositories
{
    public class NhibernateRepository<T> : NhibernateRepository<T, int>, IRepository<T> where T : IAggregateRoot<int>
    {
        public NhibernateRepository(NhibernateUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
