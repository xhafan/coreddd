using System;
using System.Data;

namespace CoreDdd.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        void Commit();
        void Rollback();
    }
}