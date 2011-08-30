using System.Globalization;
using EmailMaker.Domain.EmailTemplates;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class EmailTemplateMap : ClassMap<EmailTemplate>
    {
        public EmailTemplateMap()
        {
            Id(x => x.Id)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .GeneratedBy.HiLo("100");

            HasMany(x => x.Parts)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .KeyColumn("EmailTemplateId")
                .AsList(a => a.Column("Position"))
                .Cascade.AllDeleteOrphan();

            Map(x => x.Name);
            Map(x => x.UserId);
        }
    }
}
