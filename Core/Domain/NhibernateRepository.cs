using System;
using System.Collections.Generic;
using Core.Infrastructure;
using NHibernate;

namespace Core.Domain
{
    public class NhibernateRepository<T> : IRepository<T> where T : IAggregateRoot
    {
        private readonly ISession _session;

        public NhibernateRepository()
        {
            _session = UnitOfWork.CurrentSession;
        }

        public NhibernateRepository(ISession session)
        {
            _session = session;
        }

        public T GetById(int id)
        {
            return _session.Get<T>(id);
        }

        public IEnumerable<T> GetByIds(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public T Load(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(T objectToSave)
        {
            _session.Save(objectToSave);
        }

        public void Delete(T objectToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
