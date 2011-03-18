using EmailMaker.Domain.Emails;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class VariableEmailPartMap : SubclassMap<VariableEmailPart>
    {
        public VariableEmailPartMap()
        {
            KeyColumn("Id");
            References(x => x.VariableType, "VariableTypeId");
            Map(x => x.Value).Length(10000);
        }
    }
}