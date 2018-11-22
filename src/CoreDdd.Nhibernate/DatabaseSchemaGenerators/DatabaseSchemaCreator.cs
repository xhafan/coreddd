using System.Data.Common;
using CoreDdd.Nhibernate.Configurations;
using NHibernate.Tool.hbm2ddl;

namespace CoreDdd.Nhibernate.DatabaseSchemaGenerators
{
    /// <summary>
    /// Creates the database schema (tables, foreign keys) from the entities and their NHibernate database mappings.
    /// </summary>
    public class DatabaseSchemaCreator
    {
        /// <summary>
        /// Creates the database schema.
        /// </summary>
        /// <param name="nhibernateConfigurator">An instance of NHibernate configurator</param>
        /// <param name="connection">An opened connection</param>
        public void CreateDatabaseSchema(
            INhibernateConfigurator nhibernateConfigurator,
            DbConnection connection
            )
        {
            var configuration = nhibernateConfigurator.GetConfiguration();
            new SchemaExport(configuration).Execute(
                useStdOut: true,
                execute: true,
                justDrop: false,
                connection: connection,
                exportOutput: null
            );
        }
    }
}