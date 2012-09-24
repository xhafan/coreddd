using System.Collections.Generic;
using System.Reflection;
using CoreDdd.Domain;
using CoreDdd.Infrastructure;
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
            UnitOfWork.Initialize(GetNhibernateConfigurator());
        }

        public static INhibernateConfigurator GetNhibernateConfigurator(bool mapDtoAssembly = true)
        {
            var assembliesToMap = new List<Assembly> {typeof (Email).Assembly};
            if (mapDtoAssembly) assembliesToMap.Add(typeof (EmailDto).Assembly);
            return new NhibernateConfigurator(
                assembliesToMap.ToArray(),
                new[]
                    {
                        typeof (Entity<,>),
                        typeof (EmailPart),
                        typeof (EmailState),
                        typeof (EmailTemplatePart)
                    },
                new[]
                    {
                        typeof (EmailState)
                    },
                true,
                typeof (EmailStateSubclassConvention).Assembly);
        }
    }
}