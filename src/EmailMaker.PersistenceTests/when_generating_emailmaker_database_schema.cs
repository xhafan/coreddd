using EmailMaker.Infrastructure;
using NUnit.Framework;

namespace EmailMaker.PersistenceTests
{
    [TestFixture]
    public class when_generating_emailmaker_database_schema
    {
        [Test]
        public void GenerateSchema()
        {
            new EmailMakerDatabaseSchemaGenerator(@".\schema.sql").Generate();
        }
    }
}
