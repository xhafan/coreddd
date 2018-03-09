using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Commands;

namespace EmailMaker.Commands
{
    public class CommandHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn(typeof(ICommandHandler<>))
                    .WithService.FirstInterface()
                    .Configure(x => x.LifestyleTransient()));
        }
    }
}