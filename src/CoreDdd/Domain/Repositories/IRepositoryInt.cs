namespace CoreDdd.Domain.Repositories;

/// <summary>
/// Represents a repository to load/save/delete aggregate root entities with an id of type int.
/// </summary>
/// <typeparam name="TAggregateRoot">An aggregate root entity type with an id of type int</typeparam>
public interface IRepositoryInt<TAggregateRoot> : IRepository<TAggregateRoot, int> 
    where TAggregateRoot : EntityInt, IAggregateRoot;