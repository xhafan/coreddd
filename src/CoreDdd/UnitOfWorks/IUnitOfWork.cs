using System;

namespace CoreDdd.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Flush();
        bool IsActive();
    }
}