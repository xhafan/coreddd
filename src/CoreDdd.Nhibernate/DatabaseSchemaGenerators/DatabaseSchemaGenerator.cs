using System;
using System.IO;
using CoreDdd.Nhibernate.Configurations;
using NHibernate.Tool.hbm2ddl;

namespace CoreDdd.Nhibernate.DatabaseSchemaGenerators
{
    public class DatabaseSchemaGenerator
    {
        private readonly string _databaseSchemaFileName;
        private readonly INhibernateConfigurator _nhibernateConfigurator;

        public DatabaseSchemaGenerator(
            string databaseSchemaFileName,
            INhibernateConfigurator nhibernateConfigurator
            )
        {
            _nhibernateConfigurator = nhibernateConfigurator;
            _databaseSchemaFileName = databaseSchemaFileName;
        }

        public void Generate()
        {
            File.Delete(_databaseSchemaFileName);
            var schemaExport = new SchemaExport(_nhibernateConfigurator.GetConfiguration());
            schemaExport.SetOutputFile(_databaseSchemaFileName);
            schemaExport.Create(true, false);
            if (!File.Exists(_databaseSchemaFileName) || new FileInfo(_databaseSchemaFileName).Length == 0)
            {
                throw new Exception("Error generating database schema");
            }
        }
    }
}
