using CoreDdd.UnitOfWorks;
using Ninject;
using Ninject.Syntax;

namespace CoreDdd.Nhibernate.Register.Ninject
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
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
            // do nothing - Ninject does not have a concept of releasing components like Castle Windsor
        }
    }
}