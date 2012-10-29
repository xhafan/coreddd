using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.Domain.EmailTemplates
{
    public class EmailTemplateAutoMap : IAutoMappingOverride<EmailTemplate>
    {
        public void Override(AutoMapping<EmailTemplate> mapping)
        {
            mapping.HasMany(x => x.Parts)
                .AsList(a => a.Column("Position"));
            mapping.Map(x => x.Name).Nullable();
        }
    }
}