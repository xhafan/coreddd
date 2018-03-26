namespace CoreDdd.Domain.Repositories
{
    public interface IRepository<T, in TId> 
        where T : IAggregateRoot
    {
        T Get(TId id);
        T Load(TId id);
        void Save(T objectToSave);
        void Delete(T objectToDelete);
    }
}