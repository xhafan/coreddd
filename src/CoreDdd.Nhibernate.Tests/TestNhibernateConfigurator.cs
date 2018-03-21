using System.Reflection;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.Tests.TestEntities;
using HibernatingRhinos.Profiler.Appender.NHibernate;

namespace CoreDdd.Nhibernate.Tests
{
    public class TestNhibernateConfigurator : NhibernateConfigurator
    {
        public TestNhibernateConfigurator()
            : base(false)
        {
#if DEBUG
            NHibernateProfiler.Initialize();
#endif
        }

        protected override Assembly[] GetAssembliesToMap(bool mapDtoAssembly)
        {
            return new[] { typeof(EqualityEntity).Assembly };
        }
    }
}