using CoreDdd.Commands;
using Ninject.Modules;

namespace CoreDdd.Register.Ninject
{
    public class CommandExecutorBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandExecutor>().To<CommandExecutor>().InTransientScope();
        }
    }
}
