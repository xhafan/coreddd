using EmailMaker.Domain.EmailTemplates.VariableTypes;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class VariableTypeMap : ClassMap<VariableType>
    {
        public VariableTypeMap()
        {
            Id(x => x.Id)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .GeneratedBy.HiLo("100");

            Map(x => x.Name);
        }
    }
}