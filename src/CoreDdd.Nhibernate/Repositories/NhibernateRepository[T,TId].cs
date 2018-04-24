using System;
using System.Threading.Tasks;
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

        public Task<T> GetAsync(TId id)
        {
#if NET40
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.GetAsync<T>(id);
#endif
        }

        public T Load(TId id)
        {
            return _unitOfWork.Session.Load<T>(id);
        }

        public Task<T> LoadAsync(TId id)
        {
#if NET40
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.LoadAsync<T>(id);
#endif
        }

        public void Save(T objectToSave)
        {
            _unitOfWork.Session.Save(objectToSave);
        }

        public Task SaveAsync(T objectToSave)
        {
#if NET40
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.SaveAsync(objectToSave);
#endif
        }

        public void Delete(T objectToDelete)
        {
            _unitOfWork.Session.Delete(objectToDelete);
        }

        public Task DeleteAsync(T objectToDelete)
        {
#if NET40
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.DeleteAsync(objectToDelete);
#endif
        }

        private NotSupportedException _GetAsyncNotSupportedException()
        {
            return new NotSupportedException("Async methods are supported only for .NET 4.6.1+ . For lower .NET versions, please use sync method instead.");
        }

    }
}