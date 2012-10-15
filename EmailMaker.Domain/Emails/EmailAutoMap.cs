using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.Domain.Emails
{
    public class EmailAutoMap : IAutoMappingOverride<Email>
    {
        public void Override(AutoMapping<Email> mapping)
        {
            mapping.HasMany(x => x.Parts)
                .AsList(a => a.Column("Position"));
            mapping.Map(x => x.FromAddress).Nullable();
            mapping.Map(x => x.Subject).Nullable();
        }
    }
}