using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.Domain.Emails.EmailStates
{
    public class EmailStateAutoMap : IAutoMappingOverride<EmailState>
    {
        public void Override(AutoMapping<EmailState> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.Assigned();
            mapping.DiscriminateSubClassesOnColumn("Name");
        }
    }
}