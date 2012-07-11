using System;
using EmailMaker.Domain.Emails.EmailStates;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace EmailMaker.Domain.Conventions
{
    // todo: move it from here, remove Convetions folder in domain
    public class SubclassConvention : ISubclassConvention, ISubclassConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<ISubclassInspector> criteria)
        {
            criteria.Expect(subclass => Type.GetType(subclass.Name).BaseType == typeof(EmailState));
        }

        public void Apply(ISubclassInstance instance)
        {
            if (instance.Name == typeof(DraftEmailState).AssemblyQualifiedName) instance.DiscriminatorValue(EmailState.Draft.Name);
            if (instance.Name == typeof(ToBeSentEmailState).AssemblyQualifiedName) instance.DiscriminatorValue(EmailState.ToBeSent.Name);
            if (instance.Name == typeof(SentEmailState).AssemblyQualifiedName) instance.DiscriminatorValue(EmailState.Sent.Name);
        }
    }
}