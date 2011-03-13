using System;

namespace Core.Commands
{
    public class CommandExecutedArgs : EventArgs
    {
        public object Args { get; set; }
    }
}