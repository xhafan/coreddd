using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CoreDdd.Infrastructure;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using EmailMaker.Infrastructure;
using NUnit.Framework;

namespace EmailMaker.IntegrationTests
{
    [SetUpFixture]
    public class RunOncePerTestRun
    {
        [SetUp]
        public void SetUp()
        {
            var container = new WindsorContainer();
            container.Register(
                Component.For<INhibernateConfigurator>()
                    .ImplementedBy<EmailMakerNhibernateConfigurator>()
                    .LifeStyle.Singleton,
                Component.For<NhibernateUnitOfWork>()
                    .ImplementedBy<NhibernateUnitOfWork>()
                    .LifeStyle.PerThread
                );
            IoC.Initialize(container);
        }
    }
}