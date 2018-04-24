using System.Threading.Tasks;

namespace CoreDdd.Domain.Repositories
{
    public interface IRepository<T, in TId> 
        where T : IAggregateRoot
    {
        T Get(TId id);
        Task<T> GetAsync(TId id);

        T Load(TId id);
        Task<T> LoadAsync(TId id);

        void Save(T objectToSave);
        Task SaveAsync(T objectToSave);

        void Delete(T objectToDelete);
        Task DeleteAsync(T objectToDelete);
    }
}