using Castle.Windsor;
using Castle.Windsor.Installer;
using CoreDdd.Nhibernate.Register.Castle;
using CoreIoC;
using CoreIoC.Castle;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.PersistenceTests
{
    [SetUpFixture]
    public class RunOncePerTestRun
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            NhibernateInstaller.SetUnitOfWorkLifeStyle(x => x.PerThread);

            var container = new WindsorContainer();
            container.Install(
                FromAssembly.Containing<NhibernateInstaller>(),
                FromAssembly.Containing<TestNhibernateInstaller>()
                );
            IoC.Initialize(new CastleContainer(container));
        }
    }
}