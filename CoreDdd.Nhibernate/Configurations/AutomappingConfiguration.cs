using System;
using CoreDdd.Domain;
using CoreUtils.Extensions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Utils;

namespace CoreDdd.Nhibernate.Configurations
{
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        private readonly Type[] _discriminatedTypes;

        public AutomappingConfiguration(params Type[] discriminatedTypes)
        {
            _discriminatedTypes = discriminatedTypes;
        }

        public override bool ShouldMap(Type type)
        {
            return HasNotIgnoreAttribute(type) && (IsDomainEntity(type) || IsDto(type));
        }

        public override bool IsDiscriminated(Type type)
        {
            return type.In(_discriminatedTypes);
        }

        private bool IsDto(Type type)
        {
            return typeof(IAutoMappedDto).IsAssignableFrom(type);
        }

        private static bool IsDomainEntity(Type type)
        {
            return type.IsSubclassOfRawGeneric(typeof(Entity<>));
        }

        private bool HasNotIgnoreAttribute(Type type)
        {
            return Attribute.GetCustomAttribute(type, typeof (IgnoreAutoMapAttribute)) == null;                
        }
    }
}