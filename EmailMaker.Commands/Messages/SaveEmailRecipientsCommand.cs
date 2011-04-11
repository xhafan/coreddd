using Core.Commands;

namespace EmailMaker.Commands.Messages
{
    public class SaveEmailRecipientsCommand : ICommand
    {
        public int EmailId { get; set; }
        public string FromAddress { get; set; }
        public string ToAddressesStr { get; set; }
    }
}