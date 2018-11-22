using CoreDdd.Domain;
using CoreUtils.Extensions;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions
{
    internal class PrimaryKeyConvention : IIdConvention, IIdConventionAcceptance
    {
        private static string _maxLo;

        public static void Initialize(string maxLo)
        {
            _maxLo = maxLo;
        }

        public void Accept(IAcceptanceCriteria<IIdentityInspector> criteria)
        {
            criteria.Expect(x => x.EntityType.IsSubclassOfRawGeneric(typeof (Entity<>)));
        }

        public void Apply(IIdentityInstance instance)
        {
            if (instance.Type == typeof(int) 
                || instance.Type == typeof(long))
            {
                instance.Column("Id");
                instance.GeneratedBy.HiLo(_maxLo);
            }
        }
    }
}