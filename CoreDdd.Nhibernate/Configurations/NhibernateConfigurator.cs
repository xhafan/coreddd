using System;
using System.Collections.Generic;
using System.Configuration;
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
            var autoPersistenceModel = AutoMap.Assemblies(new AutomappingConfiguration(discriminatedTypes.ToArray()), assembliesToMap);
            includeBaseTypes.Each(x => autoPersistenceModel.IncludeBase(x));
            ignoreBaseTypes.Each(x => autoPersistenceModel.IgnoreBase(x));
            assembliesToMap.Each(x => autoPersistenceModel.UseOverridesFromAssembly(x));
            if (mapDefaultConventions) autoPersistenceModel.Conventions.AddFromAssemblyOf<PrimaryKeyConvention>();
            assemblyWithAdditionalConventions.Each(x => autoPersistenceModel.Conventions.AddAssembly(x));

            _sessionFactory = Fluently.Configure(_configuration)
                .Mappings(x =>
                              {
                                  var mappingsContainer = x.AutoMappings.Add(autoPersistenceModel);
                                  var exportNhibernateMappingsFolder = ConfigurationManager.AppSettings["ExportNhibernateMappingsFolder"];
                                  if (!string.IsNullOrWhiteSpace(exportNhibernateMappingsFolder)) mappingsContainer.ExportTo(exportNhibernateMappingsFolder);
                              })
                .BuildSessionFactory();
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
