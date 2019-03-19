using System;
using System.Collections.Generic;
using System.Linq;
using CoreDdd.Domain;
using CoreUtils.Extensions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Utils;

namespace CoreDdd.Nhibernate.Configurations
{
    /// <summary>
    /// Default CoreDdd.Nhibernate automapping configuration.
    /// You can derive from this class and override <see cref="BaseNhibernateConfigurator.GetAutomappingConfiguration"/> method
    /// to do further automapping configuration customizations.
    /// </summary>
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        private readonly Type[] _discriminatedTypes;
        private readonly Func<Type, bool> _isTypeDto;
        private readonly bool _shouldMapDtos;
        private readonly Type[] _componentTypes;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="discriminatedTypes">Types where the whole inheritance hierarchy is mapped into one table with a discriminating column</param>
        /// <param name="isTypeDto">A function determining if a type is a dto (data transfer object)</param>
        /// <param name="shouldMapDtos">A flag indicating whether dtos should be mapped into a database</param>
        /// <param name="componentTypes">Types representing NHibernate components</param>
        public AutomappingConfiguration(
            IEnumerable<Type> discriminatedTypes,
            Func<Type, bool> isTypeDto, 
            bool shouldMapDtos,
            IEnumerable<Type> componentTypes
            )
        {
            _discriminatedTypes = discriminatedTypes.ToArray();
            _isTypeDto = isTypeDto;
            _shouldMapDtos = shouldMapDtos;
            _componentTypes = componentTypes.ToArray();
        }

        /// <summary>
        /// Determines whether a type should be mapped into a database.
        /// </summary>
        /// <param name="type">Given type</param>
        /// <returns>True if the type should be mapped into a database; false otherwise</returns>
        public override bool ShouldMap(Type type)
        {
            return IsDomainEntity(type) || IsDto(type) || IsComponent(type);
        }

        private bool IsDto(Type type)
        {
            return _shouldMapDtos && _isTypeDto(type);
        }

        private bool IsDomainEntity(Type type)
        {
            return type.IsSubclassOfRawGeneric(typeof(Entity<>));
        }

        /// <summary>
        /// Determines if a type is NHibernate component.
        /// </summary>
        /// <param name="type">Given type</param>
        /// <returns>True if the type is NHibernate component; false otherwise</returns>
        public override bool IsComponent(Type type)
        {
            return type.In(_componentTypes);
        }

        /// <summary>
        /// Determines if a type is discriminated (=the whole inheritance hierarchy is mapped into one table with a discriminating column)
        /// </summary>
        /// <param name="type">Given type</param>
        /// <returns>True if the type is discriminated; false otherwise</returns>
        public override bool IsDiscriminated(Type type)
        {
            return type.In(_discriminatedTypes);
        }
    }
}