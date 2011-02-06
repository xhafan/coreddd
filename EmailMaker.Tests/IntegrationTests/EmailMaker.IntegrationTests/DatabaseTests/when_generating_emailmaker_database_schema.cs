using Core.TestHelper.Persistence;
using EmailMaker.Domain.NHibernateMappings;
using NUnit.Framework;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    [TestFixture]
    public class when_generating_emailmaker_database_schema : base_when_generating_database_schema
    {
        protected override void SetUp()
        {
            DatabaseSchemaFileName = "..\\..\\..\\..\\..\\EmailMaker.Database\\EmailMaker_generated_database_schema.sql";
            AssemblyWithMappings = typeof (EmailTemplateMap).Assembly;
        }
    }
}
