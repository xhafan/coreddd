using Core.Domain;
using Core.Infrastructure;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Dtos.Emails;
using EmailMaker.Infrastructure.Conventions;

namespace EmailMaker.Infrastructure
{
    public static class UnitOfWorkInitializer
    {
        public static void Initialize()
        {
            UnitOfWork.Initialize(
                new NhibernateConfigurator(
                    new[]
                        {
                            typeof (Email).Assembly,
                            typeof (EmailDto).Assembly
                        },
                    new[]
                        {
                            typeof (Identity<>),
                            typeof (EmailPart),
                            typeof (EmailState),
                            typeof (EmailTemplatePart)
                        },
                    new[]
                        {
                            typeof (EmailState)
                        },
                    true,
                    typeof (EmailStateSubclassConvention).Assembly));
        }
    }
}