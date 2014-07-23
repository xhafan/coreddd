using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using CoreDdd.Nhibernate.Conventions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;

namespace CoreDdd.Nhibernate.Configurations
{
    public abstract class NhibernateConfigurator : INhibernateConfigurator
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly Configuration _configuration;

        protected abstract Assembly[] GetAssembliesToMap();
        protected abstract IEnumerable<Type> GetIncludeBaseTypes();
        protected abstract IEnumerable<Type> GetDiscriminatedTypes();
        protected abstract bool ShouldMapDefaultConventions();
        protected abstract Assembly GetAssemblyWithAdditionalConventions();

        protected NhibernateConfigurator()
        {
            var assembliesToMap = GetAssembliesToMap();
            var includeBaseTypes = GetIncludeBaseTypes();
            var discriminatedTypes = GetDiscriminatedTypes();
            var mapDefaultConventions = ShouldMapDefaultConventions();
            var assemblyWithAdditionalConventions = GetAssemblyWithAdditionalConventions();

#if(DEBUG)
            NHibernateProfiler.Initialize(); // todo: remove nhibernate profiler
#endif
            _configuration = new Configuration();
            _configuration.Configure();
            var autoPersistenceModel = AutoMap.Assemblies(new AutomappingConfiguration(discriminatedTypes.ToArray()), assembliesToMap);
            includeBaseTypes.Each(x => autoPersistenceModel.IncludeBase(x));
            assembliesToMap.Each(x => autoPersistenceModel.UseOverridesFromAssembly(x));
            if (mapDefaultConventions) autoPersistenceModel.Conventions.AddFromAssemblyOf<PrimaryKeyConvention>();
            if (assemblyWithAdditionalConventions != null) autoPersistenceModel.Conventions.AddAssembly(assemblyWithAdditionalConventions);
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
