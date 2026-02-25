namespace CoreDdd.Domain.Repositories;

/// <summary>
/// Represents a repository to load/save/delete aggregate root entities with an id of type long.
/// For an id of type int use <see cref="IRepositoryInt{TAggregateRoot}"/>.
/// </summary>
/// <typeparam name="TAggregateRoot">An aggregate root entity type with an id of type long</typeparam>
public interface IRepository<TAggregateRoot> : IRepository<TAggregateRoot, long> 
    where TAggregateRoot : Entity, IAggregateRoot;