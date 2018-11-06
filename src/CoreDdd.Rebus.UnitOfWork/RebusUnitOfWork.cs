using System;
using System.Data;
using CoreDdd.UnitOfWorks;
using Rebus.Pipeline;

namespace CoreDdd.Rebus.UnitOfWork
{
    public static class RebusUnitOfWork
    {
        private static IUnitOfWorkFactory _unitOfWorkFactory;
        private static IsolationLevel _isolationLevel;

        public static void Initialize(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
        )
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _isolationLevel = isolationLevel;
        }

        public static IUnitOfWork Create(IMessageContext arg)
        {
            if (_unitOfWorkFactory == null)
            {
                throw new InvalidOperationException(
                    "RebusUnitOfWork has not been initialized! Please call RebusUnitOfWork.Initialize(...) before using it.");
            }
            var unitOfWork = _unitOfWorkFactory.Create();
            unitOfWork.BeginTransaction(_isolationLevel);
            return unitOfWork;
        }

        public static void Commit(IMessageContext messageContext, IUnitOfWork unitOfWork)
        {
            unitOfWork.Commit();
        }

        public static void Rollback(IMessageContext messageContext, IUnitOfWork unitOfWork)
        {
            unitOfWork.Rollback();
        }

        public static void Cleanup(IMessageContext messageContext, IUnitOfWork unitOfWork)
        {
            _unitOfWorkFactory.Release(unitOfWork);
        }
    }
}