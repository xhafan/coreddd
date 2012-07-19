using Core.Domain;
using Core.Domain.Persistence;
using Core.Infrastructure;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Dtos.Emails;
using EmailMaker.Infrastructure.Conventions;
using NHibernate;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    public static class EmailMakerPersistenceTestHelper
    {
        public static ISession ConfigureNHibernate()
        {
            // todo: search for NHibernateConfigurator usages and refactor it into one call
            UnitOfWork.Initialize(new NhibernateConfigurator(
                                      new[]
                                          {
                                              typeof (Email).Assembly, 
                                              typeof (EmailDto).Assembly
                                          },
                                      new[]
                                          {
                                              typeof (Identity<>),
                                              typeof (EmailPart),
                                              typeof (EmailTemplatePart),
                                              typeof (EmailState)
                                          },
                                      new[]
                                          {
                                              typeof(EmailState)
                                          },
                                      typeof(SubclassConvention).Assembly));
            return UnitOfWork.CurrentSession;
        }

        public static void ClearDatabase(ISession session)
        {
            using (var tx = session.BeginTransaction())
            {
                session.Delete("from Email");
                session.Delete("from EmailTemplate");
                session.Delete("from Recipient");
                session.Delete("from User");
                tx.Commit();
            }            
        }
    }
}