using EmailMaker.Domain.Emails;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class EmailMap : ClassMap<Email>
    {
        public EmailMap()
        {
            Id(x => x.Id)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .GeneratedBy.HiLo("100");

            References(x => x.EmailTemplate, "EmailTemplateId");
            HasMany(x => x.Parts)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .KeyColumn("EmailId")
                .AsList(a => a.Column("Position"))
                .Cascade.AllDeleteOrphan();
            Map(x => x.FromAddress);
            Map(x => x.Subject);
            References(x => x.State, "EmailStateId");

            HasMany(x => x.EmailRecipients)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .KeyColumn("EmailId")
                .AsSet()
                .Cascade.AllDeleteOrphan();
        }
    }
}