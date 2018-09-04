#if !NETCOREAPP
using System;
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
    public class when_saving_entity_and_throwing_exception_within_unit_of_work
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

            try
            {
                _simulateApplicationTransactionWhichThrowsAnException();
            }
            catch
            {
                httpApplication.FireError();
            }
        }

        private void _simulateApplicationTransactionWhichThrowsAnException()
        {
            _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();

            _entity = new TestEntityWithDomainEvent();
            _entityRepository.Save(_entity);

            throw new NotSupportedException("test exception");
        }

        [Test]
        public void entity_is_not_persisted_and_cannot_be_fetched_after_the_transaction_rollback()
        {
            _entity.ShouldNotBeNull();

            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            unitOfWork.BeginTransaction();

            _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();
            _entity = _entityRepository.Get(_entity.Id);

            _entity.ShouldBeNull();

            unitOfWork.Rollback();
        }

        [Test]
        public void domain_event_is_not_handled()
        {
            _raisedDomainEvent.ShouldBeNull();
        }
    }
}
#endif