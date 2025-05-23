using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using NHibernate;
using System;

#if !NET40 && !NET45
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.Repositories;

/// <summary>
/// NHibernate repository to load, save and delete aggregate root domain entity.
/// </summary>
/// <param name="unitOfWork">An instance of NHibernate unit of work</param>
/// <typeparam name="TAggregateRoot">An aggregate root domain entity type</typeparam>
/// <typeparam name="TId">An aggregate root domain entity Id type</typeparam>
public class NhibernateRepository<TAggregateRoot, TId>(NhibernateUnitOfWork unitOfWork) : IRepository<TAggregateRoot, TId> 
    where TAggregateRoot : Entity<TId>, IAggregateRoot
{
    /// <summary>
    /// Gets the NHibernate session associated with the current unit of work.
    /// </summary>
    /// <exception cref="Exception">Thrown when the NHibernate session is null.</exception>
    // ReSharper disable once MemberCanBePrivate.Global
    protected ISession Session
    {
        get
        {
            var session = unitOfWork.Session;
            if (session == null)
            {
                throw new Exception("UnitOfWork Session is null.");
            }

            return session;
        }
    }

    /// <summary>
    /// Fetches an aggregate root domain entity from the database. Does a database hit.
    /// </summary>
    /// <param name="id">An aggregate root entity id</param>
    /// <returns>An aggregate root entity, or null when not found</returns>
    public TAggregateRoot? Get(TId id)
    {
        return Session.Get<TAggregateRoot>(id);
    }

    /// <summary>
    /// Fetches an aggregate root domain entity from the database using a lock. Does a database hit.
    /// </summary>
    /// <param name="id">An aggregate root entity id</param>
    /// <param name="lockMode">Required lock mode</param>
    /// <returns>An aggregate root entity, or null when not found</returns>
    public TAggregateRoot? Get(TId id, LockMode lockMode)
    {
        return Session.Get<TAggregateRoot>(id, lockMode);
    }

    /// <summary>
    /// Fetches an aggregate root domain entity from the database. Does a database hit. Throws an exception if the aggregate root domain entity is not found.
    /// </summary>
    /// <param name="id">An aggregate root entity id</param>
    /// <returns>An aggregate root entity.</returns>
    /// <exception cref="Exception">Thrown when the entity with the given id not found.</exception>
    public TAggregateRoot LoadById(TId id)
    {
        var aggregateRootEntity = Get(id);
        if (aggregateRootEntity == null)
        {
            throw new Exception($"{typeof(TAggregateRoot).Name} Id {id} not found.");
        }
        return aggregateRootEntity;
    }
    
    /// <summary>
    /// Returns an aggregate root entity proxy without the database hit. The aggregate root proxy 
    /// does a database hit to fetch the real data when the aggregate root is accessed.
    /// See https://stackoverflow.com/a/2125711/379279.
    /// </summary>
    /// <remarks>Throws an exception when the object is accessed and the entity is not found</remarks>
    /// <param name="id">An aggregate root entity id</param>
    /// <returns>An aggregate root domain entity proxy</returns>
    public TAggregateRoot Load(TId id)
    {
        return Session.Load<TAggregateRoot>(id);
    }

    /// <summary>
    /// Returns an aggregate root entity proxy without the database hit. The aggregate root proxy 
    /// does a database hit using a lock to fetch the real data when the aggregate root is accessed.
    /// See https://stackoverflow.com/a/2125711/379279.
    /// </summary>
    /// <remarks>Throws an exception when the object is accessed and the entity is not found</remarks>
    /// <param name="id">An aggregate root entity id</param>
    /// <param name="lockMode">Required lock mode</param>
    /// <returns>An aggregate root domain entity proxy</returns>
    public TAggregateRoot Load(TId id, LockMode lockMode)
    {
        return Session.Load<TAggregateRoot>(id, lockMode);
    }    

    /// <summary>
    /// Saves an aggregate root domain entity into the NHibernate session, and generates the entity Id.
    /// When the session is flushed (commit flushes the session automatically), the entity is inserted or updated in the database.
    /// When the entity is transient (=newed up, not previously saved into the database), and it's Id is generated
    /// by the database (e.g. SQL server identity), the entity is inserted into the DB immediately, and not during the flush.
    /// </summary>
    /// <param name="aggregateRoot">An aggregate root entity</param>
    public void Save(TAggregateRoot aggregateRoot)
    {
        Session.Save(aggregateRoot);
    }
    
    /// <summary>
    /// Deletes an aggregate root domain entity from the NHibernate session.
    /// When the session is flushed (commit flushes the session automatically), the entity is deleted from the database.
    /// </summary>
    public void Delete(TAggregateRoot aggregateRoot)
    {
        Session.Delete(aggregateRoot);
    }    
    
#if !NET40 && !NET45
    /// <summary>
    /// Fetches an aggregate root domain entity asynchronously from the database. Does a database hit.
    /// </summary>
    /// <param name="id">An aggregate root entity id</param>
    /// <returns>An aggregate root entity, or null when not found</returns>
    public async Task<TAggregateRoot?> GetAsync(TId id)
    {
        return await Session.GetAsync<TAggregateRoot>(id);
    }

    /// <summary>
    /// Fetches an aggregate root domain entity asynchronously from the database using a lock. Does a database hit.
    /// </summary>
    /// <param name="id">An aggregate root entity id</param>
    /// <param name="lockMode">Required lock mode</param>
    /// <returns>An aggregate root entity, or null when not found</returns>
    public async Task<TAggregateRoot?> GetAsync(TId id, LockMode lockMode)
    {
        return await Session.GetAsync<TAggregateRoot>(id, lockMode);
    }

    /// <summary>
    /// Fetches an aggregate root domain entity from the database. Does a database hit. Throws an exception if the aggregate root domain entity is not found.
    /// </summary>
    /// <param name="id">An aggregate root entity id</param>
    /// <returns>An aggregate root entity.</returns>
    /// <exception cref="Exception">Thrown when the entity with the given id not found.</exception>
    public async Task<TAggregateRoot> LoadByIdAsync(TId id)
    {
        var aggregateRootEntity = await GetAsync(id);
        if (aggregateRootEntity == null)
        {
            throw new Exception($"{typeof(TAggregateRoot).Name} Id {id} not found.");
        }
        return aggregateRootEntity;
    }

    /// <summary>
    /// Returns asynchronously an aggregate root entity proxy without the database hit. The aggregate root proxy 
    /// does a database hit to fetch the real data when the aggregate root is accessed.
    /// See https://stackoverflow.com/a/2125711/379279.
    /// </summary>
    /// <remarks>Throws an exception when the object is accessed and the entity is not found</remarks>
    /// <param name="id">An aggregate root entity id</param>
    /// <returns>An aggregate root domain entity proxy</returns>
    public Task<TAggregateRoot> LoadAsync(TId id)
    {
        return Session.LoadAsync<TAggregateRoot>(id);
    }

    /// <summary>
    /// Returns asynchronously an aggregate root entity proxy without the database hit. The aggregate root proxy 
    /// does a database hit with a lock to fetch the real data when the aggregate root is accessed.
    /// See https://stackoverflow.com/a/2125711/379279.
    /// </summary>
    /// <remarks>Throws an exception when the object is accessed and the entity is not found</remarks>
    /// <param name="id">An aggregate root entity id</param>
    /// <param name="lockMode">Required lock mode</param>
    /// <returns>An aggregate root domain entity proxy</returns>
    public Task<TAggregateRoot> LoadAsync(TId id, LockMode lockMode)
    {
        return Session.LoadAsync<TAggregateRoot>(id, lockMode);
    }

    /// <summary>
    /// Saves an aggregate root domain entity asynchronously into the NHibernate session, and generates the entity Id.
    /// When the session is flushed (commit flushes the session automatically), the entity is inserted or updated in the database.
    /// When the entity is transient (=newed up, not previously saved into the database), and it's Id is generated
    /// by the database (e.g. SQL server identity), the entity is inserted into the DB immediately, and not during the flush.
    /// </summary>
    public Task SaveAsync(TAggregateRoot aggregateRoot)
    {
        return Session.SaveAsync(aggregateRoot);
    }
    
    /// <summary>
    /// Deletes an aggregate root domain entity asynchronously from the NHibernate session.
    /// When the session is flushed (commit flushes the session automatically), the entity is deleted from the database.
    /// </summary>
    public Task DeleteAsync(TAggregateRoot aggregateRoot)
    {
        return Session.DeleteAsync(aggregateRoot);
    }
#endif
}