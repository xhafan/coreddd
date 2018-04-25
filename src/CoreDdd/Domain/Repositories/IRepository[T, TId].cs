#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Domain.Repositories
{
    public interface IRepository<T, in TId> 
        where T : IAggregateRoot
    {
        T Get(TId id);
        T Load(TId id);
        void Save(T objectToSave);
        void Delete(T objectToDelete);

#if !NET40
        Task<T> LoadAsync(TId id);
        Task<T> GetAsync(TId id);
        Task SaveAsync(T objectToSave);
        Task DeleteAsync(T objectToDelete);
#endif
    }
}