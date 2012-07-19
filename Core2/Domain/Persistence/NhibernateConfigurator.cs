using System;
using System.Collections.Generic;
using System.Reflection;
using Core.Domain.Persistence.Conventions;
using Core.Infrastructure;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;

namespace Core.Domain.Persistence
{
    public class NhibernateConfigurator : INhibernateConfigurator
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly Configuration _configuration;

        public NhibernateConfigurator(Assembly[] assembliesToMap, IEnumerable<Type> includeBaseTypes, Type[] discriminatedTypes, Assembly assemblyWithConventions)
        {
#if(DEBUG)
            NHibernateProfiler.Initialize();
#endif
            _configuration = new Configuration();
            _configuration.Configure();
            var autoPersistenceModel = AutoMap.Assemblies(new AutomappingConfiguration(discriminatedTypes), assembliesToMap);
            includeBaseTypes.Each(x => autoPersistenceModel.IncludeBase(x));
            assembliesToMap.Each(x => autoPersistenceModel.UseOverridesFromAssembly(x));
            autoPersistenceModel
                .Conventions.AddFromAssemblyOf<PrimaryKeyConvention>() // todo: Add conventions from assembly; make it core.net independent
                .Conventions.AddAssembly(assemblyWithConventions)
                ;
            _sessionFactory = Fluently.Configure(_configuration)
                .Mappings(m => m.AutoMappings.Add(autoPersistenceModel).ExportTo(@"d:\temp\export")) // todo: fix path
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
