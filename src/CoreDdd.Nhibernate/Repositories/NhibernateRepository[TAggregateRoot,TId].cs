using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;

#if !NET40
using System;
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.Repositories
{
    public class NhibernateRepository<TAggregateRoot, TId> : IRepository<TAggregateRoot, TId> 
        where TAggregateRoot : Entity<TId>, IAggregateRoot
    {
        private readonly NhibernateUnitOfWork _unitOfWork;

        public NhibernateRepository(NhibernateUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TAggregateRoot Get(TId id)
        {
            return _unitOfWork.Session.Get<TAggregateRoot>(id);
        }

#if !NET40
        public Task<TAggregateRoot> GetAsync(TId id)
        {
#endif
#if NET40
#elif NET45
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.GetAsync<TAggregateRoot>(id);
#endif
#if !NET40
        }
#endif

        public TAggregateRoot Load(TId id)
        {
            return _unitOfWork.Session.Load<TAggregateRoot>(id);
        }

#if !NET40
        public Task<TAggregateRoot> LoadAsync(TId id)
        {
#endif
#if NET40
#elif NET45
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.LoadAsync<TAggregateRoot>(id);
#endif
#if !NET40
        }
#endif

        public void Save(TAggregateRoot aggregateRoot)
        {
            _unitOfWork.Session.Save(aggregateRoot);
        }

#if !NET40
        public Task SaveAsync(TAggregateRoot aggregateRoot)
        {
#endif
#if NET40
#elif NET45
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.SaveAsync(aggregateRoot);
#endif
#if !NET40
        }
#endif

        public void Delete(TAggregateRoot aggregateRoot)
        {
            _unitOfWork.Session.Delete(aggregateRoot);
        }

#if !NET40
        public Task DeleteAsync(TAggregateRoot aggregateRoot)
        {
#endif
#if NET40
#elif NET45
            throw _GetAsyncNotSupportedException();
#else
            return _unitOfWork.Session.DeleteAsync(aggregateRoot);
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