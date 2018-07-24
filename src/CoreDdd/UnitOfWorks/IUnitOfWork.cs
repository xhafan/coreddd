using System;
using System.Data;

#if !NET40 && !NET45
using System.Threading.Tasks;
#endif

namespace CoreDdd.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        void Commit();
        void Rollback();

#if !NET40 && !NET45
        Task CommitAsync();
        Task RollbackAsync();
#endif
    }
}