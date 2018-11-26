using CoreDdd.Domain;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;

namespace CoreDdd.Nhibernate.TestHelpers
{
    /// <summary>
    /// <see cref="NhibernateUnitOfWork"/> extension methods.
    /// </summary>
    public static class NhibernateUnitOfWorkExtensions // todo: rename the file to NhibernateUnitOfWorkExtensions
    {
        /// <summary>
        /// Saves an aggregate root domain entity using NhibernateRepository.
        /// </summary>
        /// <typeparam name="TAggregateRoot">An aggregate root domain entity type</typeparam>
        /// <param name="unitOfWork">NHibernate unit of work</param>
        /// <param name="aggregateRoot">An instance of aggregate root domain entity</param>
        public static void Save<TAggregateRoot>(this NhibernateUnitOfWork unitOfWork, TAggregateRoot aggregateRoot) 
            where TAggregateRoot : Entity, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(unitOfWork);
            repository.Save(aggregateRoot);
            unitOfWork.Flush();
        }

        /// <summary>
        /// Saves an aggregate root domain entity using NhibernateRepository.
        /// </summary>
        /// <typeparam name="TAggregateRoot">An aggregate root domain entity type</typeparam>
        /// <typeparam name="TId">Entity Id type</typeparam>
        /// <param name="unitOfWork">NHibernate unit of work</param>
        /// <param name="aggregateRoot">An instance of aggregate root domain entity</param>
        public static void Save<TAggregateRoot, TId>(this NhibernateUnitOfWork unitOfWork, TAggregateRoot aggregateRoot) 
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(unitOfWork);
            repository.Save(aggregateRoot);
            unitOfWork.Flush();
        }

        /// <summary>
        /// Fetches an aggregate root domain entity from the database using NhibernateRepository.
        /// </summary>
        /// <typeparam name="TAggregateRoot">An aggregate root domain entity type</typeparam>
        /// <param name="unitOfWork">NHibernate unit of work</param>
        /// <param name="id">An aggregate root domain entity id</param>
        /// <returns>An aggregate root domain entity, or null when not found</returns>
        public static TAggregateRoot Get<TAggregateRoot>(this NhibernateUnitOfWork unitOfWork, int id) 
            where TAggregateRoot : Entity, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(unitOfWork);
            return repository.Get(id);
        }

        /// <summary>
        /// Fetches an aggregate root domain entity from the database using NhibernateRepository.
        /// </summary>
        /// <typeparam name="TAggregateRoot">An aggregate root domain entity type</typeparam>
        /// <typeparam name="TId">Entity Id type</typeparam>
        /// <param name="unitOfWork">NHibernate unit of work</param>
        /// <param name="id">An aggregate root domain entity id</param>
        /// <returns>An aggregate root domain entity, or null when not found</returns>
        public static TAggregateRoot Get<TAggregateRoot, TId>(this NhibernateUnitOfWork unitOfWork, TId id) 
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(unitOfWork);
            return repository.Get(id);
        }

        /// <summary>
        /// Loads an aggregate root domain entity proxy using NhibernateRepository.
        /// </summary>
        /// <typeparam name="TAggregateRoot">An aggregate root domain entity type</typeparam>
        /// <param name="unitOfWork">NHibernate unit of work</param>
        /// <param name="id">An aggregate root domain entity id</param>
        /// <returns>An aggregate root domain entity proxy</returns>
        public static TAggregateRoot Load<TAggregateRoot>(this NhibernateUnitOfWork unitOfWork, int id) 
            where TAggregateRoot : Entity, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(unitOfWork);
            return repository.Load(id);
        }

        /// <summary>
        /// Loads an aggregate root domain entity proxy using NhibernateRepository.
        /// </summary>
        /// <typeparam name="TAggregateRoot">An aggregate root domain entity type</typeparam>
        /// <typeparam name="TId">Entity Id type</typeparam>
        /// <param name="unitOfWork">NHibernate unit of work</param>
        /// <param name="id">An aggregate root domain entity id</param>
        /// <returns>An aggregate root domain entity proxy</returns>
        public static TAggregateRoot Load<TAggregateRoot, TId>(this NhibernateUnitOfWork unitOfWork, TId id) 
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(unitOfWork);
            return repository.Load(id);
        }

        /// <summary>
        /// Deletes an aggregate root domain entity using NhibernateRepository.
        /// </summary>
        /// <typeparam name="TAggregateRoot">An aggregate root domain entity type</typeparam>
        /// <param name="unitOfWork">NHibernate unit of work</param>
        /// <param name="aggregateRoot">An instance of aggregate root domain entity</param>
        public static void Delete<TAggregateRoot>(this NhibernateUnitOfWork unitOfWork, TAggregateRoot aggregateRoot)
            where TAggregateRoot : Entity, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(unitOfWork);
            repository.Delete(aggregateRoot);
            unitOfWork.Flush();
        }

        /// <summary>
        /// Deletes an aggregate root domain entity using NhibernateRepository.
        /// </summary>
        /// <typeparam name="TAggregateRoot">An aggregate root domain entity type</typeparam>
        /// <typeparam name="TId">Entity Id type</typeparam>
        /// <param name="unitOfWork">NHibernate unit of work</param>
        /// <param name="aggregateRoot">An instance of aggregate root domain entity</param>
        public static void Delete<TAggregateRoot, TId>(this NhibernateUnitOfWork unitOfWork, TAggregateRoot aggregateRoot)
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(unitOfWork);
            repository.Delete(aggregateRoot);
            unitOfWork.Flush();
        }
    }
}