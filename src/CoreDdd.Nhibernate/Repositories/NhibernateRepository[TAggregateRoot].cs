using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;

namespace CoreDdd.Nhibernate.Repositories
{
    /// <summary>
    /// NHibernate repository to load, save and delete aggregate root domain entity with an id of type int.
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public class NhibernateRepository<TAggregateRoot> : NhibernateRepository<TAggregateRoot, int>, IRepository<TAggregateRoot> 
        where TAggregateRoot : Entity, IAggregateRoot
    {
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="unitOfWork">An instance of NHibernate unit of work</param>
        public NhibernateRepository(NhibernateUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
