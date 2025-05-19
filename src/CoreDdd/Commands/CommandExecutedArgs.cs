namespace CoreDdd.Commands
{
    /// <summary>
    /// A class to contain a command executed event data.
    /// </summary>
    public class CommandExecutedArgs
    {
        /// <summary>
        /// Command executed event data. For instance store here a generated id of a newly created aggregate root entity.
        /// </summary>
        public object? Args { get; set; }
    }
}