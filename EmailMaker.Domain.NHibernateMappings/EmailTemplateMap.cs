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

            HasMany(x => x.Names)
                .Table("EmailTemplateForCulture")
                .KeyColumn("EmailTemplateId")
                .Element("Name")
                .AsMap<CultureInfo>("Culture")
                .Cascade.AllDeleteOrphan();
        }
    }
}
