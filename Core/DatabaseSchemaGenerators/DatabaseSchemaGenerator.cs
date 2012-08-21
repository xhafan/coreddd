using System;
using System.IO;
using Core.Infrastructure;
using NHibernate.Tool.hbm2ddl;

namespace Core.DatabaseSchemaGenerators
{
    public abstract class DatabaseSchemaGenerator
    {
        protected abstract string GetDatabaseSchemaFileName();
        protected abstract INhibernateConfigurator GetNhibernateConfigurator();

        public void Generate()
        {
            var databaseSchemaFileName = GetDatabaseSchemaFileName();
            File.Delete(databaseSchemaFileName);
            var nHibernateConfigurator = GetNhibernateConfigurator();
            var schemaExport = new SchemaExport(nHibernateConfigurator.GetConfiguration());
            schemaExport.SetOutputFile(databaseSchemaFileName);
            schemaExport.Create(true, false);
            if (!File.Exists(databaseSchemaFileName) || new FileInfo(databaseSchemaFileName).Length == 0) throw new Exception("Error generating database schema");
        }
    }
}
