using System;
#if !NET40 && !NET45
using System.Threading.Tasks;
#endif 

namespace CoreDdd.Nhibernate.UnitOfWorks;

/// <summary>
/// Runs an operation inside a unit of work with automatic transaction handling.
/// </summary>
public static class NhibernateUnitOfWorkRunner
{
#if !NET40 && !NET45
    /// <summary>
    /// Runs an async operation inside a unit of work with automatic transaction handling.
    /// Commits on success, rolls back on failure, and disposes the session safely.
    /// </summary>
    public static async Task RunAsync(
        Func<NhibernateUnitOfWork> unitOfWorkFactoryFunc,
        Func<NhibernateUnitOfWork, Task> operationAction
    )
    {
        using var unitOfWork = unitOfWorkFactoryFunc();
        unitOfWork.BeginTransaction();
    
        try
        {
            await operationAction(unitOfWork);
            await unitOfWork.CommitAsync();
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Runs an operation returning a result inside a unit of work with automatic transaction handling.
    /// Commits on success, rolls back on failure, and disposes the session safely.
    /// </summary>
    public static async Task<TResult> RunAsync<TResult>(
        Func<NhibernateUnitOfWork> unitOfWorkFactoryFunc,
        Func<NhibernateUnitOfWork, Task<TResult>> operationFunc
    )
    {
        using var unitOfWork = unitOfWorkFactoryFunc();
        unitOfWork.BeginTransaction();

        try
        {
            var result = await operationFunc(unitOfWork);
            await unitOfWork.CommitAsync();
            return result;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
#endif

    /// <summary>
    /// Runs an operation inside a unit of work with automatic transaction handling.
    /// Commits on success, rolls back on failure, and disposes the session safely.
    /// </summary>
    public static void Run(
        Func<NhibernateUnitOfWork> unitOfWorkFactoryFunc,
        Action<NhibernateUnitOfWork> operationAction
    )
    {
        using var unitOfWork = unitOfWorkFactoryFunc();
        unitOfWork.BeginTransaction();

        try
        {
            operationAction(unitOfWork);
            unitOfWork.Commit();
        }
        catch
        {
            unitOfWork.Rollback();
            throw;
        }
    }

    /// <summary>
    /// Runs an operation returning a result inside a unit of work with automatic transaction handling.
    /// Commits on success, rolls back on failure, and disposes the session safely.
    /// </summary>
    public static TResult Run<TResult>(
        Func<NhibernateUnitOfWork> unitOfWorkFactoryFunc,
        Func<NhibernateUnitOfWork, TResult> operationFunc
    )
    {
        using var unitOfWork = unitOfWorkFactoryFunc();
        unitOfWork.BeginTransaction();

        try
        {
            var result = operationFunc(unitOfWork);
            unitOfWork.Commit();
            return result;
        }
        catch
        {
            unitOfWork.Rollback();
            throw;
        }
    }
}