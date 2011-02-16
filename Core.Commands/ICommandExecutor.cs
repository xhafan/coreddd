namespace Core.Commands
{
    public interface ICommandExecutor
    {
        void Execute<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage;
    }
}
