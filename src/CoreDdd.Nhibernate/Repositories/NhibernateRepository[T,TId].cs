using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;

namespace CoreDdd.Nhibernate.Repositories
{
    public class NhibernateRepository<T, TId> : IRepository<T, TId> where T : IAggregateRoot
    {
        private readonly NhibernateUnitOfWork _unitOfWork;

        public NhibernateRepository(NhibernateUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T Get(TId id)
        {
            return _unitOfWork.Session.Get<T>(id);
        }

        public T Load(TId id)
        {
            return _unitOfWork.Session.Load<T>(id);
        }

        public void Save(T objectToSave)
        {
            _unitOfWork.Session.Save(objectToSave);
        }

        public void Delete(T objectToDelete)
        {
            _unitOfWork.Session.Delete(objectToDelete);
        }
    }
}