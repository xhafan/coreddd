using CoreDdd.Infrastructure;
using NServiceBus;

namespace EmailMaker.Service
{
    public class UnitOfWorkMessageModule : IMessageModule
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkMessageModule(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void HandleBeginMessage()
        {
            _unitOfWork.BeginTransaction();
        }

        public void HandleEndMessage()
        {
            _unitOfWork.Commit();
        }

        public void HandleError()
        {
            _unitOfWork.Rollback();
        }
    }
}
