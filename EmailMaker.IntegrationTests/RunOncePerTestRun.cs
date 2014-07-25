using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using CoreIoC.Castle;
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
            var windsorContainer = new WindsorContainer();
            windsorContainer.Register(
                Component.For<INhibernateConfigurator>()
                    .ImplementedBy<EmailMakerNhibernateConfigurator>()
                    .LifeStyle.Singleton,
                Component.For<NhibernateUnitOfWork>()
                    .ImplementedBy<NhibernateUnitOfWork>()
                    .LifeStyle.PerThread
                );
            IoC.Initialize(new CastleContainer(windsorContainer));
        }
    }
}