using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreDdd.Nhibernate.Conventions;
using CoreDdd.Nhibernate.DatabaseSchemaGenerators;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Utils;
using NHibernate;
using NHibernate.Cfg;
using Configuration = NHibernate.Cfg.Configuration;

namespace CoreDdd.Nhibernate.Configurations
{
    /// <summary>
    /// NHibernate configuration base class. Derive your custom NHibernate configuration from this class.
    /// Allows to configure various NHibernate settings.
    /// Override method GetAssembliesToMap() to define assemblies with domain entities and DTOS (data transfer objects) to map into a database.
    /// </summary>
    public abstract class BaseNhibernateConfigurator : INhibernateConfigurator, IDisposable
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly Configuration _configuration;

        /// <summary>
        /// Initialize the instance.
        /// </summary>
        /// <param name="shouldMapDtos">A flag defining whether DTOs (data transfer objects) should be mapped into a database.
        /// It's handy to set it to false when generating a database script using <see cref="DatabaseSchemaGenerator"/>
        /// so it would not create database views for DTOs as tables.</param>
        /// <param name="configurationFileName">The location of the XML file to use to configure NHibernate</param>
        protected BaseNhibernateConfigurator(
            bool shouldMapDtos = true,
            string configurationFileName = null
            )
        {
            var assembliesToMap = GetAssembliesToMap();
            var includeBaseTypes = GetIncludeBaseTypes();
            var ignoreBaseTypes = GetIgnoreBaseTypes();
            var discriminatedTypes = GetDiscriminatedTypes();
            var shouldUseDefaultConventions = ShouldUseDefaultConventions();
            var assemblyWithAdditionalConventions = GetAssembliesWithAdditionalConventions();
            
            _configuration = new Configuration();
            if (string.IsNullOrWhiteSpace(configurationFileName))
            {
                _configuration.Configure();
            }
            else
            {
                _configuration.Configure(configurationFileName);
            }
            var autoPersistenceModel = AutoMap.Assemblies(
                new AutomappingConfiguration(discriminatedTypes.ToArray(), GetFuncToDetermineIfTypeIsDto(), shouldMapDtos), 
                assembliesToMap
                );
            includeBaseTypes.Each(x => autoPersistenceModel.IncludeBase(x));
            ignoreBaseTypes.Each(x => autoPersistenceModel.IgnoreBase(x));
            assembliesToMap.Each(x => autoPersistenceModel.UseOverridesFromAssembly(x));
            if (shouldUseDefaultConventions)
            {
                DisableLazyLoadForDtosConvention.Initialize(GetFuncToDetermineIfTypeIsDto());
                HasManyForDomainConvention.Initialize(
                    GetBackingFieldNameFromPropertyNameFunc(),
                    GetCollectionInstanceAccessAction()
                    );
                PrimaryKeyConvention.Initialize(GetIdentityHiLoMaxLo());

                autoPersistenceModel.Conventions.AddFromAssemblyOf<PrimaryKeyConvention>();
            }
            assemblyWithAdditionalConventions.Each(x => autoPersistenceModel.Conventions.AddAssembly(x));

            _configuration.SetNamingStrategy(GetNamingStrategy());

            var fluentConfiguration = Fluently.Configure(_configuration)
                .Mappings(x =>
                              {
                                  var mappingsContainer = x.AutoMappings.Add(autoPersistenceModel);
                                  var exportNhibernateMappingsFolder = GetExportNhibernateMappingsFolder();
                                  if (!string.IsNullOrWhiteSpace(exportNhibernateMappingsFolder))
                                  {
                                      mappingsContainer.ExportTo(exportNhibernateMappingsFolder);
                                  }
                              });
            _sessionFactory = fluentConfiguration.BuildSessionFactory();
        }

        /// <summary>
        /// Gets a list of assemblies with entities and DTOs which should be mapped into a database.
        /// </summary>
        /// <returns></returns>
        protected abstract Assembly[] GetAssembliesToMap();

        /// <summary>
        /// Include abstract entity or DTO types in the inheritance hierarchy to be mapped into a database.
        /// </summary>
        /// <returns>A collection of entity and DTO types</returns>
        protected virtual IEnumerable<Type> GetIncludeBaseTypes()
        {
            yield break;
        }

        /// <summary>
        /// Ignore non-abstract entity and DTO types in the inheritance hierarchy from mapping into a database.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Type> GetIgnoreBaseTypes()
        {
            yield break;
        }

        /// <summary>
        /// Gets types where to whole inheritance hierarchy is mapped into one table with a type discriminating column.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Type> GetDiscriminatedTypes()
        {
            yield break;
        }

        /// <summary>
        /// A flag whether the default conventions (see <see cref="PrimaryKeyConvention"/> and others) should be used.
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldUseDefaultConventions()
        {
            return true;
        }

        /// <summary>
        /// Gets a list of assemblies with additional NHibernate conventions.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Assembly> GetAssembliesWithAdditionalConventions()
        {
            yield break;
        }
        
        /// <summary>
        /// Override this method to change the default way how to determine if a type is a DTO.
        /// </summary>
        /// <returns></returns>
        protected virtual Func<Type, bool> GetFuncToDetermineIfTypeIsDto()
        {
            return type => type.Name.EndsWith("Dto");
        }

        protected virtual Func<string, string> GetBackingFieldNameFromPropertyNameFunc()
        {
            return propertyName =>
            {
                var firstLetterLower = char.ToLower(propertyName[0]);
                var propertyNameWithoutTheFirstLetter = propertyName.Substring(1);
                return $"_{firstLetterLower}{propertyNameWithoutTheFirstLetter}";
            };
        }

        protected virtual Action<IAccessInstance> GetCollectionInstanceAccessAction()
        {
            return accessInstance => accessInstance.ReadOnlyPropertyThroughCamelCaseField(CamelCasePrefix.Underscore);
        }

        protected virtual string GetIdentityHiLoMaxLo()
        {
            return null; // todo: move 100 here
        }

        protected virtual string GetExportNhibernateMappingsFolder()
        {
            return null;
        }

        protected virtual INamingStrategy GetNamingStrategy()
        {
            return new DoubleQuoteIdentifiersNamingStrategy();
        }

        public ISessionFactory GetSessionFactory()
        {
            return _sessionFactory;
        }

        public Configuration GetConfiguration()
        {
            return _configuration;
        }

        public void Dispose() // https://stackoverflow.com/a/898867/379279
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sessionFactory.Dispose();
            }
        }
    }
}
