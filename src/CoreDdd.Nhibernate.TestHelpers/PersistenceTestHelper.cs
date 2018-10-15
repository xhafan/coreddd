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

        public void Save<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(UnitOfWork);
            repository.Save(entity);
            Flush();
        }

        public void SaveGeneric<TAggregateRoot, TId>(TAggregateRoot entity) where TAggregateRoot : IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(UnitOfWork);
            repository.Save(entity);
            Flush();
        }

        public TAggregateRoot Get<TAggregateRoot>(int id) where TAggregateRoot : IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(UnitOfWork);
            return repository.Get(id);
        }

        public TAggregateRoot GetGeneric<TAggregateRoot, TId>(TId id) where TAggregateRoot : IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(UnitOfWork);
            return repository.Get(id);
        }

        public TAggregateRoot Load<TAggregateRoot>(int id) where TAggregateRoot : IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(UnitOfWork);
            return repository.Load(id);
        }

        public TAggregateRoot LoadGeneric<TAggregateRoot, TId>(TId id) where TAggregateRoot : IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(UnitOfWork);
            return repository.Load(id);
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