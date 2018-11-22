using System;
using System.IO;
using CoreDdd.Nhibernate.Configurations;
using NHibernate.Tool.hbm2ddl;

namespace CoreDdd.Nhibernate.DatabaseSchemaGenerators
{
    /// <summary>
    /// Generates a database schema (tables, foreign keys) SQL script from the entities and their NHibernate database mappings.
    /// To avoid generating tables for DTO (data transfer object) database views, configure NHibernate configurator without
    /// DTO mappings (see <see cref="BaseNhibernateConfigurator"/>, constructor parameter shouldMapDtos)
    /// </summary>
    public class DatabaseSchemaGenerator
    {
        private readonly string _databaseSchemaFileName;
        private readonly INhibernateConfigurator _nhibernateConfigurator;

        /// <summary>
        /// Initialized the instance.
        /// </summary>
        /// <param name="databaseSchemaFileName">A file name with the generated SQL script</param>
        /// <param name="nhibernateConfigurator">An instance of NHibernate configurator</param>
        public DatabaseSchemaGenerator(
            string databaseSchemaFileName,
            INhibernateConfigurator nhibernateConfigurator
            )
        {
            _nhibernateConfigurator = nhibernateConfigurator;
            _databaseSchemaFileName = databaseSchemaFileName;
        }

        /// <summary>
        /// Generates the SQL script.
        /// </summary>
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
