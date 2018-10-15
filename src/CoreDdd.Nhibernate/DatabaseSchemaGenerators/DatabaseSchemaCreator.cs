using System.Data.Common;
using CoreDdd.Nhibernate.Configurations;
using NHibernate.Tool.hbm2ddl;

namespace CoreDdd.Nhibernate.DatabaseSchemaGenerators
{
    public class DatabaseSchemaCreator
    {
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