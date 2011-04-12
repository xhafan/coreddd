using Core.TestHelper.Extensions;
using Core.Utilities.Extensions;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Domain.EmailTemplates;

namespace EmailMaker.TestHelper.Builders
{

    public class EmailBuilder
    {       
        private int _nextPartId;
        private int _id;
        private EmailTemplate _emailTemplate;
        private EmailState _state = EmailState.Draft;

        private int NextPartId
        {
            get
            {
                _nextPartId++;
                return _nextPartId;
            }
        }

        public static EmailBuilder New
        {
            get
            {
                return new EmailBuilder();
            }
        }

        public EmailBuilder WithEmailTemplate(EmailTemplate emailTemplate)
        {
            _emailTemplate = emailTemplate;
            return this;
        }

        public EmailBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public EmailBuilder WithState(EmailState state)
        {
            _state = state;
            return this;
        }

        public Email Build()
        {
            var email = new Email(_emailTemplate);
            email.SetPrivateAttribute("_id", _id);
            email.Parts.Each(part => part.SetPrivateAttribute("_id", NextPartId));
            email.SetPrivateProperty("State", _state);
            return email;
        }
    }
}