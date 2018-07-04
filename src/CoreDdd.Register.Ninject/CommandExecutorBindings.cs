using CoreDdd.Commands;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace CoreDdd.Register.Ninject
{
    public class CommandExecutorBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandHandlerFactory>().ToFactory();
            Bind<ICommandExecutor>().To<CommandExecutor>().InTransientScope();
        }
    }
}
