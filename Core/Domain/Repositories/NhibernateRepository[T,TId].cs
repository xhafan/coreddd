using System;
using System.Collections.Generic;
using Core.Infrastructure;
using NHibernate;

namespace Core.Domain.Repositories
{
    public class NhibernateRepository<T, TId> : IRepository<T, TId> where T : IAggregateRoot<TId>
    {
        protected ISession Session;

        public NhibernateRepository()
        {
            Session = UnitOfWork.CurrentSession;
        }

        public NhibernateRepository(ISession session)
        {
            Session = session;
        }

        public T GetById(TId id)
        {
            return Session.Get<T>(id);
        }

        public IEnumerable<T> GetByIds(IEnumerable<TId> ids)
        {
            throw new NotImplementedException();
        }

        public T Load(TId id)
        {
            throw new NotImplementedException();
        }

        public void Save(T objectToSave)
        {
            Session.Save(objectToSave);
        }

        public void Delete(T objectToDelete)
        {
            throw new NotImplementedException();
        }
    }
}