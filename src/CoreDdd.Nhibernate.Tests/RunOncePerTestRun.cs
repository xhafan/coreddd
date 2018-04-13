using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Threading;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.Register.Castle;
using CoreIoC;
using CoreIoC.Castle;
using NHibernate.Tool.hbm2ddl;
using Npgsql;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests
{
    [SetUpFixture]
    public class RunOncePerTestRun
    {
        private Mutex _mutex;

        [OneTimeSetUp]
        public void SetUp()
        {
            _acquireSynchronizationMutex();

            NhibernateInstaller.SetUnitOfWorkLifeStyle(x => x.PerThread);

            var container = new WindsorContainer();
            container.Install(
                FromAssembly.Containing<NhibernateInstaller>(),
                FromAssembly.Containing<TestNhibernateInstaller>()
                );
            IoC.Initialize(new CastleContainer(container));

            _createDatabase();

            void _acquireSynchronizationMutex()
            {
                var mutexName = GetType().Namespace;
                _mutex = new Mutex(false, mutexName);
                if (!_mutex.WaitOne(TimeSpan.FromSeconds(60)))
                {
                    throw new Exception(
                        "Timeout waiting for synchronization mutex to prevent other .net frameworks running concurrent tests over the same database");
                }
            }

            void _createDatabase()
            {
                var configuration = IoC.Resolve<INhibernateConfigurator>().GetConfiguration();
                using (var connection = _getDbConnection())
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

                DbConnection _getDbConnection()
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["CoreDddNhibernateTestsConnection"].ConnectionString;
                    var connectionDriverClass = configuration.Properties["connection.driver_class"];

                    switch (connectionDriverClass)
                    {
                        case string x when x.Contains("SQLite"):
                            return new SQLiteConnection(connectionString);
                        case string x when x.Contains("SqlClient"):
                            return new SqlConnection(connectionString);
                        case string x when x.Contains("NpgsqlDriver"):
                            return new NpgsqlConnection(connectionString);
                        default:
                            throw new Exception("Unsupported NHibernate connection.driver_class");
                    }
                }

            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _mutex.ReleaseMutex();
            _mutex.Dispose();
        }
    }
}