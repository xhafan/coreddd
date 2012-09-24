using System.Collections.Generic;

namespace CoreDdd.Domain.Repositories
{
    public interface IRepository<T, in TId> where T : IAggregateRoot<TId>
    {
        T GetById(TId id);
        IEnumerable<T> GetByIds(IEnumerable<TId> ids);
        T Load(TId id);
        void Save(T objectToSave);
        void Delete(T objectToDelete);
    }
}