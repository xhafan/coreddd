using Core.Domain;
using Core.Tests.Helpers.Persistence;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Dtos.Emails;
using EmailMaker.Infrastructure.Conventions;
using NUnit.Framework;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    [TestFixture]
    public class when_generating_emailmaker_database_schema : base_when_generating_database_schema
    {
        protected override void SetUp()
        {
            DatabaseSchemaFileName = "..\\..\\..\\EmailMaker.Database\\EmailMaker_generated_database_schema.sql";
            AssembliesToMap = new[]{ typeof (Email).Assembly, typeof(EmailDto).Assembly};
            IncludeBaseTypes = new[]
                                    {
                                        typeof (Identity<>),
                                        typeof (EmailPart),
                                        typeof (EmailTemplatePart),
                                        typeof (EmailState)
                                    };
            DiscriminatedTypes = new[]
                                     {
                                         typeof (EmailState)
                                     };
            AssemblyWithConventions = typeof (EmailStateSubclassConvention).Assembly;
        }
    }
}
