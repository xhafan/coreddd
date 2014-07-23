using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.DatabaseSchemaGenerators;

namespace EmailMaker.Infrastructure
{
    public class EmailMakerDatabaseSchemaGenerator : DatabaseSchemaGenerator
    {
        private readonly string _databaseSchemaFileName;

        public EmailMakerDatabaseSchemaGenerator(string databaseSchemaFileName)
        {
            _databaseSchemaFileName = databaseSchemaFileName;
        }


        protected override string GetDatabaseSchemaFileName()
        {
            return _databaseSchemaFileName;
        }

        protected override INhibernateConfigurator GetNhibernateConfigurator()
        {
            return new EmailMakerNhibernateConfigurator(false);
        }
    }
}
