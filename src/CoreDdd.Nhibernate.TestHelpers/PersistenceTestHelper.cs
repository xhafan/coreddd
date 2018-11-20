using CoreDdd.Domain;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;

namespace CoreDdd.Nhibernate.TestHelpers
{
    public class PersistenceTestHelper
    {
        public NhibernateUnitOfWork UnitOfWork;

        public PersistenceTestHelper(INhibernateConfigurator nhibernateConfigurator)
        {
            UnitOfWork = new NhibernateUnitOfWork(nhibernateConfigurator);
        }

        public PersistenceTestHelper(NhibernateUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void BeginTransaction()
        {
            UnitOfWork.BeginTransaction();
        }

        public void Save<TAggregateRoot>(TAggregateRoot aggregateRoot) 
            where TAggregateRoot : Entity, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(UnitOfWork);
            repository.Save(aggregateRoot);
            Flush();
        }

        public void SaveGeneric<TAggregateRoot, TId>(TAggregateRoot aggregateRoot) 
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(UnitOfWork);
            repository.Save(aggregateRoot);
            Flush();
        }

        public TAggregateRoot Get<TAggregateRoot>(int id) 
            where TAggregateRoot : Entity, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(UnitOfWork);
            return repository.Get(id);
        }

        public TAggregateRoot GetGeneric<TAggregateRoot, TId>(TId id) 
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(UnitOfWork);
            return repository.Get(id);
        }

        public TAggregateRoot Load<TAggregateRoot>(int id) 
            where TAggregateRoot : Entity, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(UnitOfWork);
            return repository.Load(id);
        }

        public TAggregateRoot LoadGeneric<TAggregateRoot, TId>(TId id) 
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(UnitOfWork);
            return repository.Load(id);
        }

        public void Delete<TAggregateRoot>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : Entity, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(UnitOfWork);
            repository.Delete(aggregateRoot);
            Flush();
        }

        public void DeleteGeneric<TAggregateRoot, TId>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(UnitOfWork);
            repository.Delete(aggregateRoot);
            Flush();
        }

        public void Commit()
        {
            UnitOfWork.Commit();
        }

        public void Rollback()
        {
            UnitOfWork.Rollback();
        }

        public void Flush()
        {
            UnitOfWork.Flush();
        }

        public void Clear()
        {
            UnitOfWork.Clear();
        }
    }
}