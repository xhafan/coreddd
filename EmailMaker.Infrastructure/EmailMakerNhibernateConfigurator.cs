using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreDdd.Nhibernate.Configurations;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Domain.EmailTemplates.VariableTypes;
using EmailMaker.Domain.NhibernateMapping.Emails;
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
            var assembliesToMap = new List<Assembly> { typeof(Email).Assembly, typeof(EmailAutoMap).Assembly };
            if (_mapDtoAssembly) assembliesToMap.Add(typeof(EmailDto).Assembly);
            return assembliesToMap.ToArray();
        }

        protected override IEnumerable<Type> GetIncludeBaseTypes()
        {
            return base.GetIncludeBaseTypes().Union(new[]
            {
                typeof (EmailPart),
                typeof (EmailState),
                typeof (EmailTemplatePart)
            });
        }

        protected override IEnumerable<Type> GetIgnoreBaseTypes()
        {
            yield return typeof(AutoTextVariableType);
            yield return typeof(InputTextVariableType);
            yield return typeof(ListVariableType);
            yield return typeof(TranslationVariableType);
        }

        protected override IEnumerable<Type> GetDiscriminatedTypes()
        {
            yield return typeof(EmailState);
        }

        protected override IEnumerable<Assembly> GetAssembliesWithAdditionalConventions()
        {
            yield return typeof(EmailStateSubclassConvention).Assembly;
        }
    }
}