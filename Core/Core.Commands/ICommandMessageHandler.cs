namespace Core.Commands
{
    public interface ICommandMessageHandler<in TCommandMessage> where TCommandMessage: ICommandMessage
    {
        void Execute(TCommandMessage command);
    }
}
