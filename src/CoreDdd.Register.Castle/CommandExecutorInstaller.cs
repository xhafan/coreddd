using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Commands;

namespace CoreDdd.Register.Castle
{
    public class CommandExecutorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICommandHandlerFactory>().AsFactory(),
                Component.For<ICommandExecutor>()
                    .ImplementedBy<CommandExecutor>()
                    .LifeStyle.Transient);
        }
    }
}