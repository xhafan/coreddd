using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Core.Commands
{
    public class CommandExecutorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICommandExecutor>()
                    .ImplementedBy<CommandExecutor>()
                    .LifeStyle.Transient);
        }
    }
}