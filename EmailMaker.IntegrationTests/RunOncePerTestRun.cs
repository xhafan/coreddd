using Castle.Windsor;
using Castle.Windsor.Installer;
using CoreDdd.Nhibernate.Register.Castle;
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
            NhibernateInstaller.SetUnitOfWorkLifeStyle(x => x.PerThread);
            var container = new WindsorContainer();
            container.Install(
                FromAssembly.Containing<EmailMakerNhibernateInstaller>(),
                FromAssembly.Containing<NhibernateInstaller>()
                );
            IoC.Initialize(new CastleContainer(container));
        }
    }
}