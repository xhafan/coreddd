using System;
using CoreDdd.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDdd.Nhibernate.Register.DependencyInjection
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public IUnitOfWork Create()
        {
            return _serviceProvider.GetService<IUnitOfWork>();
        }

        public void Release(IUnitOfWork unitOfWork)
        {
        }
    }
}