namespace CoreDdd.Domain;

/// <summary>
/// Base class for domain entities with an id of type long.
/// For an id of type int use <see cref="EntityInt"/>.
/// </summary>
public abstract class Entity : Entity<long>;