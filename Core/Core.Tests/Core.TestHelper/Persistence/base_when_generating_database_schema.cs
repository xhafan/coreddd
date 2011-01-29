using System.IO;
using System.Reflection;
using FluentNHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Core.TestHelper.Persistence
{
    public abstract class base_when_generating_database_schema
    {
        protected string DatabaseSchemaFileName;
        protected Assembly AssemblyWithMappings;

        protected abstract void SetUp();

        [TestFixtureSetUp]
        public void Context()
        {
            SetUp();
            File.Delete(DatabaseSchemaFileName);

            var configuration = new Configuration();
            configuration.Configure();
            var persistenceModel = new PersistenceModel();
            persistenceModel.AddMappingsFromAssembly(AssemblyWithMappings);
            persistenceModel.Configure(configuration);
            var schemaExport = new SchemaExport(configuration);
            schemaExport.SetOutputFile(DatabaseSchemaFileName);
            schemaExport.Create(true, false);
        }

        [Test]
        public void database_schema_generated()
        {
            File.Exists(DatabaseSchemaFileName);
        }
    }
}