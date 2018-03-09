using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using NHibernate;

namespace CoreDdd.Nhibernate.Repositories
{
    public class NhibernateRepository<T, TId> : IRepository<T, TId> where T : IAggregateRoot<TId>
    {
        private readonly ISession _session;

        public NhibernateRepository(NhibernateUnitOfWork unitOfWork)
        {
            _session = unitOfWork.Session;
        }

        public T GetById(TId id)
        {
            return _session.Get<T>(id);
        }

        public T Load(TId id)
        {
            return _session.Load<T>(id);
        }

        public void Save(T objectToSave)
        {
            _session.Save(objectToSave);
        }

        public void Delete(T objectToDelete)
        {
            _session.Delete(objectToDelete);
        }
    }
}