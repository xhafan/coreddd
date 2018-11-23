using CoreDdd.UnitOfWorks;
using Ninject;
using Ninject.Syntax;

namespace CoreDdd.Nhibernate.Register.Ninject
{
    internal class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IResolutionRoot _ninjectIoCContainer;

        public UnitOfWorkFactory(IResolutionRoot ninjectIoCContainer)
        {
            _ninjectIoCContainer = ninjectIoCContainer;
        }

        public IUnitOfWork Create()
        {
            return _ninjectIoCContainer.Get<IUnitOfWork>();
        }

        public void Release(IUnitOfWork unitOfWork)
        {
            _ninjectIoCContainer.Release(unitOfWork);
        }
    }
}