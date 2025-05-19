﻿#if !NET40
using System;
using System.Transactions;
using System.Web;
using CoreDdd.UnitOfWorks;

namespace CoreDdd.AspNet.HttpModules
{
    /// <summary>
    /// Wraps a web request inside a transaction scope.
    /// </summary>
    public class TransactionScopeUnitOfWorkHttpModule : IHttpModule
    {
        private const string TransactionScopeSessionKey = "CoreDdd_TransactionScopeUnitOfWorkHttpModule_TransactionScope";
        private const string UnitOfWorkSessionKey = "CoreDdd_TransactionScopeUnitOfWorkHttpModule_UnitOfWork";

        private static IUnitOfWorkFactory? _unitOfWorkFactory;
        private static IsolationLevel _isolationLevel;
        private static Action<TransactionScope>? _transactionScopeEnlistmentAction;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="application">An HTTP application instance</param>
        public void Init(HttpApplication application)
        {
            application.BeginRequest += Application_BeginRequest;
            application.EndRequest += Application_EndRequest;
            application.Error += Application_Error;
        }

        /// <summary>
        /// Initializes the class. Needs to be called when starting the web application.
        /// </summary>
        /// <param name="unitOfWorkFactory">A unit of work factory to create a new unit of work for each web request</param>
        /// <param name="isolationLevel">An isolation level for the transaction scope</param>
        /// <param name="transactionScopeEnlistmentAction">An enlistment action for the transaction scope. Use to enlist another resource manager
        /// into the transaction scope</param>
        public static void Initialize(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            Action<TransactionScope>? transactionScopeEnlistmentAction = null
            )
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _isolationLevel = isolationLevel;
            _transactionScopeEnlistmentAction = transactionScopeEnlistmentAction;
        }

        private void Application_BeginRequest(object source, EventArgs e)
        {
            if (_unitOfWorkFactory == null)
            {
                throw new Exception("TransactionScopeUnitOfWorkHttpModule was not initialized. Call TransactionScopeUnitOfWorkHttpModule.Initialize(..) first.");
            }

            TransactionScope = _CreateTransactionScope();
            _transactionScopeEnlistmentAction?.Invoke(TransactionScope);

            UnitOfWork = _unitOfWorkFactory.Create();
            UnitOfWork.BeginTransaction();
        }

        private void Application_EndRequest(object source, EventArgs e)
        {
            if (HttpContext.Current.Server.GetLastError() != null) return;

            if (_unitOfWorkFactory == null)
            {
                throw new Exception("TransactionScopeUnitOfWorkHttpModule was not initialized. Call TransactionScopeUnitOfWorkHttpModule.Initialize(..) first.");
            }   
            
            if (UnitOfWork == null)
            {
                throw new Exception("UnitOfWork is null.");
            }            
            
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

            if (_unitOfWorkFactory == null)
            {
                throw new Exception("TransactionScopeUnitOfWorkHttpModule was not initialized. Call TransactionScopeUnitOfWorkHttpModule.Initialize(..) first.");
            }   
            
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
                , TransactionScopeAsyncFlowOption.Enabled
            );
        }

        private TransactionScope TransactionScope
        {
            get => (TransactionScope) HttpContext.Current.Items[TransactionScopeSessionKey];
            set => HttpContext.Current.Items[TransactionScopeSessionKey] = value;
        }

        private IUnitOfWork? UnitOfWork
        {
            get => (IUnitOfWork?)HttpContext.Current.Items[UnitOfWorkSessionKey];
            set => HttpContext.Current.Items[UnitOfWorkSessionKey] = value;
        }

        /// <summary>
        /// Cleans up the resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
#endif