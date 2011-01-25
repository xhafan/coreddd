using System.IO;
using EmailMaker.Domain.EmailTemplates;
using FluentNHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    [TestFixture]
    public class when_generating_database_schema
    {
        private Configuration _configuration;
        private SchemaExport _schemaExport;
        private string _databaseSchemaFileName;

        [SetUp]
        public void Context()
        {
            _databaseSchemaFileName = "..\\..\\..\\..\\..\\EmailMaker.Database\\EmailMaker_generated_database_schema.sql";
            File.Delete(_databaseSchemaFileName);

            _configuration = new Configuration();
            _configuration.Configure();
            var persistenceModel = new PersistenceModel();
            persistenceModel.AddMappingsFromAssembly(typeof(EmailTemplate).Assembly);
            persistenceModel.Configure(_configuration);
            _schemaExport = new SchemaExport(_configuration);
            _schemaExport.SetOutputFile(_databaseSchemaFileName);
            _schemaExport.Create(true, false);
        }

        [Test]
        public void database_schema_generated()
        {
            File.Exists(_databaseSchemaFileName);
        }
    }

}
