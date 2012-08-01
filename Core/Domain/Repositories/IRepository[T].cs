namespace Core.Domain.Repositories
{
    public interface IRepository<T> : IRepository<T, int> where T : IAggregateRoot<int>
    {
    }
}
