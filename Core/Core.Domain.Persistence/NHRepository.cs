using System;
using System.Collections.Generic;
using Core.Commons;
using NHibernate;

namespace Core.Domain.Persistence
{
    public class NHRepository<T> : IRepository<T> where T : IAggregateRootEntity
    {
        private ISession _session;

        public NHRepository()
        {
            _session = UnitOfWork.CurrentSession;
        }

        public NHRepository(ISession session)
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
            throw new NotImplementedException();
        }

        public void Delete(T objectToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
