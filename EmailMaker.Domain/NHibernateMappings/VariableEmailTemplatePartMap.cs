using EmailMaker.Domain.EmailTemplates;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class VariableEmailTemplatePartMap : SubclassMap<VariableEmailTemplatePart>
    {
        public VariableEmailTemplatePartMap()
        {
            KeyColumn("Id");
            Map(x => x.Value).Length(10000);
        }
    }
}