using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreDdd.Nhibernate.Conventions;
using CoreDdd.Nhibernate.DatabaseSchemaGenerators;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Utils;
using NHibernate;
using NHibernate.Cfg;
using Configuration = NHibernate.Cfg.Configuration;
using Environment = NHibernate.Cfg.Environment;

namespace CoreDdd.Nhibernate.Configurations
{
    /// <summary>
    /// NHibernate configuration base class. Derive your custom NHibernate configuration from this class.
    /// Allows to configure various NHibernate settings.
    /// Override method <see cref="GetAssembliesToMap"/> to define assemblies with domain entities and DTOS (data transfer objects) to map into a database.
    /// Override other virtual methods to configure various NHibernate settings.
    /// Override method <see cref="ConfigureNhibernate"/> to completely customize NHibernate configuration.
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
        /// <param name="connectionString">Connecting string if not present in the XML file</param>
        protected BaseNhibernateConfigurator(
            bool shouldMapDtos = true,
            string configurationFileName = null,
            string connectionString = null
            )
        {
            ConfigureNhibernate(
                shouldMapDtos, 
                configurationFileName, 
                connectionString, 
                out _sessionFactory, 
                out _configuration
                );
        }

        /// <summary>
        /// Main NHibernate configuration method. 
        /// It configures NHibernate in an opinionated way, with an option to override various small NHibernate settings by overriding 
        /// other virtual methods.
        /// This method can be overridden to completely customize NHibernate configuration.
        /// </summary>
        /// <param name="shouldMapDtos">See the constructor</param>
        /// <param name="configurationFileName">See the constructor</param>
        /// <param name="connectionString">See the constructor</param>
        /// <param name="sessionFactory">NHibernate session factory</param>
        /// <param name="configuration">NHibernate configuration</param>
        protected virtual void ConfigureNhibernate(
            bool shouldMapDtos, 
            string configurationFileName,
            string connectionString,
            out ISessionFactory sessionFactory,
            out Configuration configuration
            )
        {           
            configuration = new Configuration();
            if (string.IsNullOrWhiteSpace(configurationFileName))
            {
                configuration.Configure();
            }
            else
            {
                configuration.Configure(configurationFileName);
            }

            var assembliesToMap = GetAssembliesToMap();
            var isTypeDto = GetIsTypeDtoFunc();

            var autoPersistenceModel = AutoMap.Assemblies(
                GetAutomappingConfiguration(shouldMapDtos, isTypeDto),
                assembliesToMap
            );
            GetIncludeBaseTypes().Each(x => autoPersistenceModel.IncludeBase(x));
            GetIgnoreBaseTypes().Each(x => autoPersistenceModel.IgnoreBase(x));
            assembliesToMap.Each(x => autoPersistenceModel.UseOverridesFromAssembly(x));

            _configureConventions();

            configuration.SetNamingStrategy(GetNamingStrategy());

            var fluentConfiguration = Fluently.Configure(configuration)
                .Mappings(x =>
                {
                    var mappingsContainer = x.AutoMappings.Add(autoPersistenceModel);
                    var exportNhibernateMappingsPath = GetExportNhibernateMappingsPath();
                    if (!string.IsNullOrWhiteSpace(exportNhibernateMappingsPath))
                    {
                        mappingsContainer.ExportTo(exportNhibernateMappingsPath);
                    }
                });

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                configuration.SetProperty(Environment.ConnectionString, connectionString);
            }

            AdditionalConfiguration(configuration);

            sessionFactory = fluentConfiguration.BuildSessionFactory();

            void _configureConventions()
            {
                if (ShouldUseDefaultConventions())
                {
                    LoadDtosAsReadonlyConvention.Initialize(isTypeDto);
                    DisableLazyLoadForDtosConvention.Initialize(isTypeDto);
                    HasManyConvention.Initialize(
                        GetCollectionCascadeInstanceAction(),
                        GetBackingFieldNameFromPropertyNameFunc(),
                        GetCollectionInstanceAccessAction()
                    );
                    PrimaryKeyConvention.Initialize(GetIdentityHiLoMaxLo());

                    var disabledConventions = GetDisabledConventions();
                    var conventionTypes = Assembly.GetExecutingAssembly().GetTypes()
                        .Where(type => typeof(IConvention).IsAssignableFrom(type)
                                       && !type.IsInterface
                                       && !disabledConventions.Contains(type))
                        .ToList();

                    conventionTypes.Each(conventionType => autoPersistenceModel.Conventions.Add(conventionType));
                }

                GetAssembliesWithAdditionalConventions().Each(assembly => autoPersistenceModel.Conventions.AddAssembly(assembly));
                GetAdditionalConventions().Each(conventionType => autoPersistenceModel.Conventions.Add(conventionType));
            }
        }

        /// <summary>
        /// Gets NHibernate session factory.
        /// </summary>
        /// <returns>NHibernate session factory</returns>
        public ISessionFactory GetSessionFactory()
        {
            return _sessionFactory;
        }

        /// <summary>
        /// Gets NHibernate configuration.
        /// </summary>
        /// <returns>NHibernate configuration</returns>
        public Configuration GetConfiguration()
        {
            return _configuration;
        }

        /// <summary>
        /// Gets an automapping configuration. 
        /// Override this methods to return customized automapping configuration.
        /// </summary>
        /// <param name="shouldMapDtos">A flag indicating whether dtos should be mapped into a database</param>
        /// <param name="isTypeDto">A function determining if a type is a dto (data transfer object)</param>
        /// <returns>An instance implementing <see cref="IAutomappingConfiguration"/></returns>
        public virtual IAutomappingConfiguration GetAutomappingConfiguration(
            bool shouldMapDtos, 
            Func<Type, bool> isTypeDto
            )
        {
            return new AutomappingConfiguration(GetDiscriminatedTypes(), isTypeDto, shouldMapDtos, GetComponentTypes());
        }

        /// <summary>
        /// Gets a list of assemblies with entities and DTOs which should be mapped into a database.
        /// </summary>
        /// <returns>A collection of assemblies</returns>
        protected abstract Assembly[] GetAssembliesToMap();

        /// <summary>
        /// Include abstract entity types in the inheritance hierarchy to be mapped into a database.
        /// </summary>
        /// <returns>A collection of entity types</returns>
        protected virtual IEnumerable<Type> GetIncludeBaseTypes()
        {
            yield break;
        }

        /// <summary>
        /// Ignore non-abstract entity types in the inheritance hierarchy from mapping into a database.
        /// </summary>
        /// <returns>A collection of entity types</returns>
        protected virtual IEnumerable<Type> GetIgnoreBaseTypes()
        {
            yield break;
        }

        /// <summary>
        /// Gets types where to whole inheritance hierarchy is mapped into one table with a discriminating column.
        /// </summary>
        /// <returns>A collection of entity types</returns>
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
        /// Override this method to register additional NHibernate conventions from assemblies.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Assembly> GetAssembliesWithAdditionalConventions()
        {
            yield break;
        }

        /// <summary>
        /// Override this method to register additional NHibernate conventions.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Type> GetAdditionalConventions()
        {
            yield break;
        }

        /// <summary>
        /// Override this method to disable some of the default conventions.
        /// Use it when you need to implement some convention differently, and the default implementation
        /// is clashing with it.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Type> GetDisabledConventions()
        {
            yield break;
        }

        /// <summary>
        /// Override this method to change the default way how to determine if a type is a DTO.
        /// </summary>
        /// <returns></returns>
        protected virtual Func<Type, bool> GetIsTypeDtoFunc()
        {
            return type => type.Name.EndsWith("Dto");
        }

        /// <summary>
        /// Configures cascading of child entity collection.
        /// </summary>
        /// <returns>An action to configure cascading</returns>
        protected virtual Action<ICollectionCascadeInstance> GetCollectionCascadeInstanceAction()
        {
            return cascade => cascade.AllDeleteOrphan();
        }

        /// <summary>
        /// Configures the default naming convention for entity collection readonly properties and their backing fields.
        /// The example of the default settings: entity collection readonly property name: ChildEntities, backing field name: _childEntities
        /// When overriding this method, method <see cref="GetCollectionInstanceAccessAction"/> needs to be overridden as well.
        /// </summary>
        /// <returns>A function which generates a backing field name from a property name</returns>
        protected virtual Func<string, string> GetBackingFieldNameFromPropertyNameFunc()
        {
            return propertyName =>
            {
                var lowerCaseFirstLetter = char.ToLower(propertyName[0]);
                var propertyNameWithoutTheFirstLetter = propertyName.Substring(1);
                return $"_{lowerCaseFirstLetter}{propertyNameWithoutTheFirstLetter}";
            };
        }

        /// <summary>
        /// By default an entity collection readonly property has a backing field.
        /// The default convention is that the backing field name is camel case name of the property name with an
        /// underscore prefix.
        /// The example of the default settings: entity collection readonly property name: ChildEntities, backing field name: _childEntities
        /// Override this method to change it. When overriding this method, you need to override
        /// <see cref="GetBackingFieldNameFromPropertyNameFunc"/> method as well.
        /// </summary>
        /// <returns></returns>
        protected virtual Action<IAccessInstance> GetCollectionInstanceAccessAction()
        {
            return accessInstance => accessInstance.ReadOnlyPropertyThroughCamelCaseField(CamelCasePrefix.Underscore);
        }

        /// <summary>
        /// Gets default maxLo for HiLo id generator.
        /// More info: https://stackoverflow.com/questions/2738671/explanation-of-nhibernate-hilo
        /// </summary>
        /// <returns>MaxLo value</returns>
        protected virtual string GetIdentityHiLoMaxLo()
        {
            const string maxLoForHiLoGenerator = "100";
            return maxLoForHiLoGenerator;
        }

        /// <summary>
        /// Gets a naming strategy for table and column names.
        /// The default is double quotes around table and column names as it works in all SQL databases,
        /// and fixes SQL errors for SQL reserved keywords.
        /// </summary>
        /// <returns>A naming strategy</returns>
        protected virtual INamingStrategy GetNamingStrategy()
        {
            return new DoubleQuoteIdentifiersNamingStrategy();
        }

        /// <summary>
        /// Gets a path to generate NHibernate mapping xml files.
        /// </summary>
        /// <returns>A path to an existing dictionary</returns>
        protected virtual string GetExportNhibernateMappingsPath()
        {
            return null;
        }

        /// <summary>
        /// Get types representing NHibernate components.
        /// </summary>
        /// <returns>A collection of types</returns>
        protected virtual IEnumerable<Type> GetComponentTypes()
        {
            yield break;
        }

        /// <summary>
        /// Override this method to add additional configuration. Can be used to set interceptors, etc.
        /// </summary>
        /// <param name="configuration">Configuration instance</param>
        protected virtual void AdditionalConfiguration(Configuration configuration)
        {
        }

        /// <summary>
        /// Disposes resources.
        /// </summary>
        public void Dispose() // https://stackoverflow.com/a/898867/379279
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Over-ridable dispose method. 
        /// </summary>
        /// <param name="disposing">true - the method call comes from a Dispose method; false - the method call comes from a finalizer</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sessionFactory.Dispose();
            }
        }
    }
}
