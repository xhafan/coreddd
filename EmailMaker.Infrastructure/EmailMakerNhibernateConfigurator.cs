using System;
using System.Collections.Generic;
using System.Reflection;
using CoreDdd.Domain;
using CoreDdd.Nhibernate.Configurations;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Dtos.Emails;
using EmailMaker.Infrastructure.Conventions;
using HibernatingRhinos.Profiler.Appender.NHibernate;

namespace EmailMaker.Infrastructure
{
    public class EmailMakerNhibernateConfigurator : NhibernateConfigurator
    {
        private readonly bool _mapDtoAssembly = true;

        public EmailMakerNhibernateConfigurator()
        {
#if(DEBUG)
            NHibernateProfiler.Initialize();
#endif
        }

        public EmailMakerNhibernateConfigurator(bool mapDtoAssembly)
            : this()
        {
            _mapDtoAssembly = mapDtoAssembly;
        }

        protected override Assembly[] GetAssembliesToMap()
        {
            var assembliesToMap = new List<Assembly> { typeof(Email).Assembly };
            if (_mapDtoAssembly) assembliesToMap.Add(typeof(EmailDto).Assembly);
            return assembliesToMap.ToArray();
        }

        protected override IEnumerable<Type> GetIncludeBaseTypes()
        {
            yield return typeof(Entity<>);
            yield return typeof(EmailPart);
            yield return typeof(EmailState);
            yield return typeof(EmailTemplatePart);
        }

        protected override IEnumerable<Type> GetDiscriminatedTypes()
        {
            yield return typeof(EmailState);
        }

        protected override bool ShouldMapDefaultConventions()
        {
            return true;
        }

        protected override Assembly GetAssemblyWithAdditionalConventions()
        {
            return typeof (EmailStateSubclassConvention).Assembly;
        }
    }
}