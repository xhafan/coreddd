using System;
using System.Transactions;
using System.Web;
using CoreDdd.UnitOfWorks;

namespace CoreDdd.AspNet.HttpModules
{
    public class TransactionScopeUnitOfWorkHttpModule : IHttpModule
    {
        private const string TransactionScopeSessionKey = "CoreDdd_TransactionScopeUnitOfWorkHttpModule_TransactionScope";
        private const string UnitOfWorkSessionKey = "CoreDdd_TransactionScopeUnitOfWorkHttpModule_UnitOfWork";

        private static IUnitOfWorkFactory _unitOfWorkFactory;
        private static IsolationLevel _isolationLevel;
        private static Action<TransactionScope> _transactionScopeEnlistmentAction;

        public void Init(HttpApplication application)
        {
            application.BeginRequest += Application_BeginRequest;
            application.EndRequest += Application_EndRequest;
            application.Error += Application_Error;
        }

        public static void Initialize(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            Action<TransactionScope> transactionScopeEnlistmentAction = null
            )
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _isolationLevel = isolationLevel;
            _transactionScopeEnlistmentAction = transactionScopeEnlistmentAction;
        }

        private void Application_BeginRequest(object source, EventArgs e)
        {
            _CheckWasInitialized();

            TransactionScope = _CreateTransactionScope();
            _transactionScopeEnlistmentAction?.Invoke(TransactionScope);

            UnitOfWork = _unitOfWorkFactory.Create();
            UnitOfWork.BeginTransaction();
        }

        private void _CheckWasInitialized()
        {
            if (_unitOfWorkFactory == null)
            {
                throw new Exception(
                    "TransactionScopeUnitOfWorkHttpModule was not initialized. Call TransactionScopeUnitOfWorkHttpModule.Initialize(..) first.");
            }
        }

        private void Application_EndRequest(object source, EventArgs e)
        {
            if (HttpContext.Current.Server.GetLastError() != null) return;

            try
            {
                UnitOfWork.Commit();
                TransactionScope.Complete();
            }
            catch
            {
                UnitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWorkFactory.Release(UnitOfWork);
                UnitOfWork = null;
                TransactionScope.Dispose();
            }
        }

        private void Application_Error(object source, EventArgs e)
        {
            if (UnitOfWork == null) return;

            try
            {
                UnitOfWork.Rollback();
            }
            finally
            {
                _unitOfWorkFactory.Release(UnitOfWork);
                UnitOfWork = null;
                TransactionScope.Dispose();
            }
        }

        private TransactionScope _CreateTransactionScope()
        {
            return new TransactionScope(
                TransactionScopeOption.Required
                , new TransactionOptions {IsolationLevel = _isolationLevel}
#if !NET40
                , TransactionScopeAsyncFlowOption.Enabled
#endif
            );
        }

        private TransactionScope TransactionScope
        {
            get => (TransactionScope) HttpContext.Current.Items[TransactionScopeSessionKey];
            set => HttpContext.Current.Items[TransactionScopeSessionKey] = value;
        }

        private IUnitOfWork UnitOfWork
        {
            get => (IUnitOfWork)HttpContext.Current.Items[UnitOfWorkSessionKey];
            set => HttpContext.Current.Items[UnitOfWorkSessionKey] = value;
        }

        public void Dispose()
        {
        }
    }
}