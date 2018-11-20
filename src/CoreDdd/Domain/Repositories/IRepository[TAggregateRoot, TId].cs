#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Domain.Repositories
{
    /// <summary>
    /// Represents a repository to load/save/delete aggregate root entities.
    /// </summary>
    /// <typeparam name="TAggregateRoot">An aggregate root entity type</typeparam>
    /// <typeparam name="TId">An aggregate root entity id type</typeparam>
    public interface IRepository<TAggregateRoot, in TId>
        where TAggregateRoot : Entity<TId>, IAggregateRoot
    {
        /// <summary>
        /// Fetches an aggregate root entity from a repository.
        /// </summary>
        /// <param name="id">An aggregate root entity id</param>
        /// <returns>An aggregate root entity, or null when not found</returns>
        TAggregateRoot Get(TId id);

        /// <summary>
        /// Returns an aggregate root entity proxy without the repository hit. The aggregate root proxy 
        /// does a repository hit to fetch the real data when the aggregate root is accessed.
        /// See https://stackoverflow.com/a/2125711/379279.
        /// <remarks>Throws an exception when the object is accessed and the entity is not found</remarks>
        /// </summary>
        /// <param name="id">An aggregate root entity id</param>
        /// <returns>An aggregate root proxy</returns>
        TAggregateRoot Load(TId id);

        /// <summary>
        /// Saves an aggregate root entity into the repository.
        /// </summary>
        /// <param name="aggregateRoot">An aggregate root entity</param>
        void Save(TAggregateRoot aggregateRoot);

        /// <summary>
        /// Deletes an aggregate root entity from the repository.
        /// </summary>
        /// <param name="aggregateRoot">An aggregate root entity</param>
        void Delete(TAggregateRoot aggregateRoot);

#if !NET40
        /// <summary>
        /// Async fetches an aggregate root entity from a repository.
        /// </summary>
        /// <param name="id">An aggregate root entity id</param>
        /// <returns>An aggregate root entity, or null when not found</returns>
        Task<TAggregateRoot> GetAsync(TId id);

        /// <summary>
        /// Async returns an aggregate root entity proxy without the repository hit. The aggregate root proxy 
        /// does a repository hit to fetch the real data when the aggregate root is accessed.
        /// See https://stackoverflow.com/a/2125711/379279.
        /// <remarks>Throws an exception when the object is accessed and the entity is not found</remarks>
        /// </summary>
        /// <param name="id">An aggregate root entity id</param>
        /// <returns>An aggregate root proxy</returns>
        Task<TAggregateRoot> LoadAsync(TId id);

        /// <summary>
        /// Async saves an aggregate root entity into the repository.
        /// </summary>
        /// <param name="aggregateRoot">An aggregate root entity</param>
        Task SaveAsync(TAggregateRoot aggregateRoot);

        /// <summary>
        /// Async deletes an aggregate root entity from the repository.
        /// </summary>
        /// <param name="aggregateRoot">An aggregate root entity</param>
        Task DeleteAsync(TAggregateRoot aggregateRoot);
#endif
    }
}