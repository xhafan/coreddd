using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreDdd.Domain;
using CoreDdd.Nhibernate.Conventions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;

namespace CoreDdd.Nhibernate.Configurations
{
    public abstract class NhibernateConfigurator : INhibernateConfigurator
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly Configuration _configuration;

        protected abstract Assembly[] GetAssembliesToMap(bool mapDtoAssembly);

        protected virtual IEnumerable<Type> GetIncludeBaseTypes()
        {
            yield return typeof(Entity<>);
        }

        protected virtual IEnumerable<Type> GetIgnoreBaseTypes()
        {
            yield break;
        }

        protected virtual IEnumerable<Type> GetDiscriminatedTypes()
        {
            yield break;
        }

        protected virtual bool ShouldMapDefaultConventions()
        {
            return true;
        }

        protected virtual IEnumerable<Assembly> GetAssembliesWithAdditionalConventions()
        {
            yield break;
        }

        protected virtual Func<Type, bool> GetFuncToDetermineIfTypeIsDto()
        {
            return null;
        }

        protected virtual Func<string, string> GetPropertyNameToBackingFieldNameFunc()
        {
            return null;
        }

        protected virtual string GetIdentityHiLoMaxLo()
        {
            return null;
        }

        protected virtual string GetExportNhibernateMappingsFolder()
        {
            return null;
        }

        protected virtual bool ShouldDoubleQuoteTableNamesForDerivedClasses(Configuration configuration)
        {
            var connectionDriverClass = configuration.Properties["connection.driver_class"];
            var isPostgreSql = connectionDriverClass.Contains("NpgsqlDriver");
            return isPostgreSql;
        }

        protected NhibernateConfigurator(bool mapDtoAssembly)
        {
            var assembliesToMap = GetAssembliesToMap(mapDtoAssembly);
            var includeBaseTypes = GetIncludeBaseTypes();
            var ignoreBaseTypes = GetIgnoreBaseTypes();
            var discriminatedTypes = GetDiscriminatedTypes();
            var mapDefaultConventions = ShouldMapDefaultConventions();
            var assemblyWithAdditionalConventions = GetAssembliesWithAdditionalConventions();
            
            _configuration = new Configuration();
            _configuration.Configure();
            var autoPersistenceModel = AutoMap.Assemblies(
                new AutomappingConfiguration(discriminatedTypes.ToArray(), GetFuncToDetermineIfTypeIsDto()), 
                assembliesToMap
                );
            includeBaseTypes.Each(x => autoPersistenceModel.IncludeBase(x));
            ignoreBaseTypes.Each(x => autoPersistenceModel.IgnoreBase(x));
            assembliesToMap.Each(x => autoPersistenceModel.UseOverridesFromAssembly(x));
            if (mapDefaultConventions)
            {
                DisableLazyLoadForDtosConvention.SetFuncToDetermineIfTypeIsDto(GetFuncToDetermineIfTypeIsDto());
                HasManyForDomainConvention.SetPropertyNameToBackingFieldNameFunc(GetPropertyNameToBackingFieldNameFunc());
                PrimaryKeyConvention.SetIdentityHiLoMaxLo(GetIdentityHiLoMaxLo());
                autoPersistenceModel.Conventions.AddFromAssemblyOf<PrimaryKeyConvention>();
            }
            assemblyWithAdditionalConventions.Each(x => autoPersistenceModel.Conventions.AddAssembly(x));

            if (ShouldDoubleQuoteTableNamesForDerivedClasses(_configuration))
            {
                _configuration.SetNamingStrategy(new QuoteTableNamesForDerivedClassesNamingStrategy());
            }

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

        public ISessionFactory GetSessionFactory()
        {
            return _sessionFactory;
        }

        public Configuration GetConfiguration()
        {
            return _configuration;
        }
    }
}
