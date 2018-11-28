using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Threading;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.DatabaseSchemaGenerators;
using CoreDdd.Nhibernate.Register.Castle;
using CoreDdd.Register.Castle;
using CoreIoC;
using CoreIoC.Castle;
using Npgsql;
using NUnit.Framework;

namespace IntegrationTestsShared
{
    public abstract class BaseRunOncePerTestRun
    {
        private Mutex _mutex;
        private WindsorContainer _container;

        protected abstract string GetSychronizationMutexName();

        [OneTimeSetUp]
        public void SetUp()
        {
            _acquireSynchronizationMutex();

            CoreDddNhibernateInstaller.SetUnitOfWorkLifeStyle(x => x.PerThread);

            _container = new WindsorContainer();

            _container.Install(
                FromAssembly.Containing<CoreDddInstaller>(),
                FromAssembly.Containing<CoreDddNhibernateInstaller>(),
                FromAssembly.Containing<TestNhibernateInstaller>()
                );

            RegisterAdditionalServices(_container);

            IoC.Initialize(new CastleContainer(_container));

            _createDatabase();

            void _acquireSynchronizationMutex()
            {
                var mutexName = GetSychronizationMutexName();
                _mutex = new Mutex(false, mutexName);
                if (!_mutex.WaitOne(TimeSpan.FromSeconds(60)))
                {
                    throw new Exception(
                        "Timeout waiting for synchronization mutex to prevent other .net frameworks running concurrent tests over the same database");
                }
            }

            void _createDatabase()
            {
                var nhibernateConfigurator = IoC.Resolve<INhibernateConfigurator>();
                var configuration = nhibernateConfigurator.GetConfiguration();                   
                using (var connection = _createDbConnection())
                {
                    connection.Open();
                    new DatabaseSchemaCreator().CreateDatabaseSchema(nhibernateConfigurator, connection);
                }

                DbConnection _createDbConnection()
                {
                    var connectionString = configuration.Properties["connection.connection_string"];
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

        protected virtual void RegisterAdditionalServices(WindsorContainer container)
        {
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _container.Dispose();

            _mutex.ReleaseMutex();
            _mutex.Dispose();
        }
    }
}