using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Commands;

namespace CoreDdd.Nhibernate.Tests.Commands
{
    public class TestCommandHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICommandHandler<TestCommand>>()
                    .ImplementedBy<TestCommandHandler>()
                    .LifeStyle.Transient
            );
        }
    }
}