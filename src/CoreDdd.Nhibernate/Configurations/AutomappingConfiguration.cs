using System;
using CoreDdd.Domain;
using CoreUtils.Extensions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Utils;

namespace CoreDdd.Nhibernate.Configurations
{
    internal class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        private readonly Type[] _discriminatedTypes;
        private readonly Func<Type, bool> _isTypeDtoFunc;

        public AutomappingConfiguration(
            Type[] discriminatedTypes,
            Func<Type, bool> isTypeDtoFunc = null
            )
        {
            _isTypeDtoFunc = isTypeDtoFunc;
            _discriminatedTypes = discriminatedTypes;
        }

        public override bool ShouldMap(Type type)
        {
            return IsDomainEntity(type) || IsDto(type);
        }

        public override bool IsDiscriminated(Type type)
        {
            return type.In(_discriminatedTypes);
        }

        private bool IsDto(Type type)
        {
            return _isTypeDtoFunc?.Invoke(type) ?? type.Name.EndsWith("Dto");
        }

        private static bool IsDomainEntity(Type type)
        {
            return type.IsSubclassOfRawGeneric(typeof(Entity<>));
        }
    }
}