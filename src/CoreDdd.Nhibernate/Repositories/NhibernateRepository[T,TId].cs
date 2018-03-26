using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;

namespace CoreDdd.Nhibernate.Repositories
{
    public class NhibernateRepository<T, TId> : IRepository<T, TId> where T : IAggregateRoot
    {
        private readonly NhibernateUnitOfWork _nhibernateUnitOfWork;

        public NhibernateRepository(NhibernateUnitOfWork unitOfWork)
        {
            _nhibernateUnitOfWork = unitOfWork;
        }

        public T Get(TId id)
        {
            return _nhibernateUnitOfWork.Session.Get<T>(id);
        }

        public T Load(TId id)
        {
            return _nhibernateUnitOfWork.Session.Load<T>(id);
        }

        public void Save(T objectToSave)
        {
            _nhibernateUnitOfWork.Session.Save(objectToSave);
        }

        public void Delete(T objectToDelete)
        {
            _nhibernateUnitOfWork.Session.Delete(objectToDelete);
        }
    }
}