using System;
using System.Data;
using NHibernate;

namespace Core.Commons
{
    public interface IUnitOfWork : IDisposable
    {
        void Flush();

        ITransaction BeginTransaction();
        ITransaction BeginTransaction(IsolationLevel isolationLevel);
        void TransactionalFlush();
        void TransactionalFlush(IsolationLevel isolationLevel);
        void TransactionalRollback();
    }
}