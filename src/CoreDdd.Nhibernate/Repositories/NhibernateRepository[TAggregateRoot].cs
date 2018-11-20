using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;

namespace CoreDdd.Nhibernate.Repositories
{
    public class NhibernateRepository<TAggregateRoot> : NhibernateRepository<TAggregateRoot, int>, IRepository<TAggregateRoot> 
        where TAggregateRoot : Entity, IAggregateRoot
    {
        public NhibernateRepository(NhibernateUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
