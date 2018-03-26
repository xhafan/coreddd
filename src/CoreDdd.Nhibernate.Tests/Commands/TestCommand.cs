using CoreDdd.Commands;

namespace CoreDdd.Nhibernate.Tests.Commands
{
    public class TestCommand : ICommand
    {
        public object CommandExecutedArgs { get; set; }
    }
}