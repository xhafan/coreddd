using System;
using System.Configuration;
using System.Data.SqlClient;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.TestHelpers;
using CoreIoC;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Configuration = NHibernate.Cfg.Configuration;

namespace CoreDdd.Nhibernate.PersistenceTests
{
    public abstract class BasePersistenceTestWithDatabaseCreation : BasePersistenceTest
    {
        [SetUp]
        public void SetUp()
        {
            CreateDatabase();

            void CreateDatabase()
            {
                var configuration = IoC.Resolve<INhibernateConfigurator>().GetConfiguration();
                var connectionDriverClass = configuration.Properties["connection.driver_class"];

                switch (connectionDriverClass)
                {
                    case string x when x.Contains("SQLite"):
                        SqlLiteCreateDatabaseWithinTheSameUnitOfWorkConnection(configuration);
                        break;
                    case string x when x.Contains("SqlClient"):
                        MsSqlCreateDatabaseWithinAnotherNonTransactionalConnection(configuration);
                        break;
                    default:
                        throw new Exception("Unsupported NHibernate connection.driver_class");
                }
            }

            void SqlLiteCreateDatabaseWithinTheSameUnitOfWorkConnection(Configuration configuration)
            {
                new SchemaExport(configuration).Execute(
                        useStdOut: true,
                        execute: true,
                        justDrop: false,
                        connection: UnitOfWork.Session.Connection,
                        exportOutput: Console.Out)
                    ;
            }

            void MsSqlCreateDatabaseWithinAnotherNonTransactionalConnection(Configuration configuration)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["CoreDddNhibernatePersistenceTestsConnection"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    new SchemaExport(configuration).Execute(
                            useStdOut: true,
                            execute: true,
                            justDrop: false,
                            connection: connection,
                            exportOutput: Console.Out)
                        ;
                }
            }
        }
    }
}