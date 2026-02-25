using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;

namespace CoreDdd.Nhibernate.Repositories;

/// <summary>
/// NHibernate repository to load, save and delete aggregate root domain entity with an id of type int.
/// </summary>
/// <typeparam name="TAggregateRoot"></typeparam>
public class NhibernateRepositoryInt<TAggregateRoot> : NhibernateRepository<TAggregateRoot, int>, IRepositoryInt<TAggregateRoot> 
    where TAggregateRoot : EntityInt, IAggregateRoot
{
    /// <summary>
    /// Initializes the instance.
    /// </summary>
    /// <param name="unitOfWork">An instance of NHibernate unit of work</param>
    public NhibernateRepositoryInt(NhibernateUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
    }
}