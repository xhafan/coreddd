#if !NETCOREAPP
using System.Web;
using CoreDdd.AspNet.HttpModules;
using CoreDdd.Domain.Events;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.TestHelpers.DomainEvents;
using CoreDdd.TestHelpers.HttpContexts;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Webs.AspNetTests.UnitOfWorkHttpModules
{
    [TestFixture]
    public class when_saving_entity_within_unit_of_work_middleware_handling
    {
        private IRepository<TestEntityWithDomainEvent> _entityRepository;
        private TestEntityWithDomainEvent _entity;
        private TestDomainEvent _raisedDomainEvent;

        [SetUp]
        public void Context()
        {
            HttpContext.Current = FakeHttpContextHelper.GetFakeHttpContext();

            var domainEventHandlerFactory = new FakeDomainEventHandlerFactory(domainEvent => _raisedDomainEvent = (TestDomainEvent)domainEvent);
            DomainEvents.Initialize(domainEventHandlerFactory, isDelayedDomainEventHandlingEnabled: true);
            DomainEvents.ResetDelayedEventsStorage();

            var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();
            UnitOfWorkHttpModule.Initialize(unitOfWorkFactory);
            var unitOfWorkHttpModule = new UnitOfWorkHttpModule();
            var httpApplication = new FakeHttpApplication();
            unitOfWorkHttpModule.Init(httpApplication);

            httpApplication.FireBeginRequest();

            _simulateApplicationTransaction();

            httpApplication.FireEndRequest();
        }

        private void _simulateApplicationTransaction()
        {
            _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();

            _entity = new TestEntityWithDomainEvent();
            _entity.BehaviouralMethodWithRaisingDomainEvent();
            
            _entityRepository.Save(_entity);
        }

        [Test]
        public void entity_is_persisted_and_can_be_fetched_after_the_transaction_commit()
        {
            _entity.ShouldNotBeNull();

            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            unitOfWork.BeginTransaction();

            _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();
            _entity = _entityRepository.Get(_entity.Id);

            _entity.ShouldNotBeNull();

            unitOfWork.Rollback();
        }

        [Test]
        public void domain_event_is_handled()
        {
            _raisedDomainEvent.ShouldNotBeNull();
        }
    }
}
#endif