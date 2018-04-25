using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;

#if !NET40
using System;
using System.Threading.Tasks;
#endif

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

#if !NET40
        public Task<T> GetAsync(TId id)
        {
#endif
#if NET40
#elif NET45
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.GetAsync<T>(id);
#endif
#if !NET40
        }
#endif

        public T Load(TId id)
        {
            return _unitOfWork.Session.Load<T>(id);
        }

#if !NET40
        public Task<T> LoadAsync(TId id)
        {
#endif
#if NET40
#elif NET45
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.LoadAsync<T>(id);
#endif
#if !NET40
        }
#endif

        public void Save(T objectToSave)
        {
            _unitOfWork.Session.Save(objectToSave);
        }

#if !NET40
        public Task SaveAsync(T objectToSave)
        {
#endif
#if NET40
#elif NET45
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.SaveAsync(objectToSave);
#endif
#if !NET40
        }
#endif

        public void Delete(T objectToDelete)
        {
            _unitOfWork.Session.Delete(objectToDelete);
        }

#if !NET40
        public Task DeleteAsync(T objectToDelete)
        {
#endif
#if NET40
#elif NET45
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.DeleteAsync(objectToDelete);
#endif
#if !NET40
        }
#endif

#if NET40
#elif NET45
        private NotSupportedException _GetAsyncNotSupportedException()
        {
            return new NotSupportedException(AsyncErrorMessageConstants.AsyncMethodNotSupportedExceptionMessage);
        }
#endif
    }
}