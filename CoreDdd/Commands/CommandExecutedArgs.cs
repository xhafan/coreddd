using System;

namespace CoreDdd.Commands
{
    public class CommandExecutedArgs : EventArgs
    {
        public object Args { get; set; }
    }
}