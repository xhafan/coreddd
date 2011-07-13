using Core.Commands;

namespace EmailMaker.Commands.Messages
{
    public class EnqueueEmailToBeSentCommand : ICommand
    {
        public int EmailId { get; set; }
        public string FromAddress { get; set; }
        public string RecipientsStr { get; set; }
        public string Subject { get; set; }
    }
}
