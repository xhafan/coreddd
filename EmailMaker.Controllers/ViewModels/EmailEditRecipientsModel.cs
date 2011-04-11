using System.Collections.Generic;

namespace EmailMaker.Controllers.ViewModels
{
    public class EmailEditRecipientsModel
    {
        public int EmailId { get; set; }
        public IEnumerable<string> FromAddresses { get; set; }
        public IEnumerable<string> ToAddresses { get; set; }
        public string Subject { get; set; }
    }
}