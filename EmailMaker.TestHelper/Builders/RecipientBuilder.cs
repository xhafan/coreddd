using Core.Tests.Helpers.Extensions;
using EmailMaker.Domain.Emails;

namespace EmailMaker.TestHelper.Builders
{
    public class RecipientBuilder
    {
        private string _emailAddress;
        private int _id;
        private string _name;

        public static RecipientBuilder New
        {
            get
            {
                return new RecipientBuilder();
            }
        }

        public RecipientBuilder WithEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        public RecipientBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public RecipientBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public Recipient Build()
        {
            var recipient = new Recipient(_emailAddress, _name);
            recipient.SetPrivateProperty(x => x.Id, _id);
            return recipient;
        }
    }
}