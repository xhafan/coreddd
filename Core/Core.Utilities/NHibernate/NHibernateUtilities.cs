using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Core.Utilities.Extensions;
using FluentNHibernate;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;

namespace Core.Utilities.NHibernate
{
    public static class NHibernateUtilities
    {
        public static ISessionFactory ConfigureNHibernate()
        {
#if(DEBUG)
            NHibernateProfiler.Initialize();
#endif
            var configuration = new Configuration();
            configuration.Configure();
            var persistenceModel = new PersistenceModel();
            var assemblies = _GetAssembliesWithMapping();
            assemblies.Each(persistenceModel.AddMappingsFromAssembly);
            persistenceModel.Configure(configuration);
            return configuration.BuildSessionFactory();
        }


        private static IEnumerable<Assembly> _GetAssembliesWithMapping()
        {
            //todo: fix this
            var assembliesWithMapping = new List<Assembly>();
            if (File.Exists("EmailMaker.Domain.dll"))
            {
                assembliesWithMapping.Add(Assembly.LoadFrom("EmailMaker.Domain.dll"));
            }
            return assembliesWithMapping;
        }

    }
}
