using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using Core.Utilities.Extensions;
using FluentNHibernate;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;

namespace Core.Utilities.NHibernate
{
    public static class NHibernateUtilities
    {
        private const string NHibernateMappingDllFileExtension = "NHibernateMappings.dll";

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
            var files = Directory.GetFiles(HttpContext.Current != null 
                                                ? HttpContext.Current.Request.PhysicalApplicationPath + "\\bin" // todo: use Server.Map()?
                                                : ".", 
                                           "*." + NHibernateMappingDllFileExtension);
            var assembliesWithMapping = new List<Assembly>();
            files.Each(file => assembliesWithMapping.Add(Assembly.LoadFrom(file)));
            return assembliesWithMapping;
        }

    }
}
