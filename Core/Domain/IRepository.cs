using System.Collections.Generic;

namespace Core.Domain
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        T GetById(int id);
        IEnumerable<T> GetByIds(IEnumerable<int> ids);
        T Load(int id);
        void Save(T objectToSave);
        void Delete(T objectToDelete);        
    }
}
