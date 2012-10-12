using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using CoreDdd.Infrastructure.Conventions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;

namespace CoreDdd.Infrastructure
{
    public class NhibernateConfigurator : INhibernateConfigurator
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly Configuration _configuration;

        public NhibernateConfigurator(
            Assembly[] assembliesToMap, 
            IEnumerable<Type> includeBaseTypes, 
            Type[] discriminatedTypes, 
            bool mapDefaultConventions,
            Assembly assemblyWithAdditionalConventions)
        {
#if(DEBUG)
            NHibernateProfiler.Initialize();
#endif
            _configuration = new Configuration();
            _configuration.Configure();
            var autoPersistenceModel = AutoMap.Assemblies(new AutomappingConfiguration(discriminatedTypes), assembliesToMap);
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
