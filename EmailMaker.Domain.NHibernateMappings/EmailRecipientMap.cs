using EmailMaker.Domain.Emails;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class EmailRecipientMap : ClassMap<EmailRecipient>
    {
        public EmailRecipientMap()
        {
            Id(x => x.Id)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .GeneratedBy.HiLo("100");

            References(x => x.Recipient, "RecipientId");
            Map(x => x.Sent);
            Map(x => x.SentDate);
        }
    }
}