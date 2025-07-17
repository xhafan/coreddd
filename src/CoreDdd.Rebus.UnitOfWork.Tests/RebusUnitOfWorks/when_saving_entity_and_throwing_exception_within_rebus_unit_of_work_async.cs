#if !NET451 
using System;
using System.Data;
using System.Threading.Tasks;
using CoreDdd.Domain.Events;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.TestHelpers.DomainEvents;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Rebus.UnitOfWork.Tests.RebusUnitOfWorks;

[TestFixture]
public class when_saving_entity_and_throwing_exception_within_rebus_unit_of_work_async
{
    private IRepository<TestEntityWithDomainEvent> _entityRepository;
    private TestEntityWithDomainEvent _entity;
    private TestDomainEvent _raisedDomainEvent;
    private FakeMessageContext _fakeMessageContext;
    private IUnitOfWork _unitOfWork;

    [SetUp]
    public async Task Context()
    {
        var domainEventHandlerFactory = new FakeDomainEventHandlerFactory(domainEvent => _raisedDomainEvent = (TestDomainEvent)domainEvent);
        DomainEvents.Initialize(domainEventHandlerFactory);
        DomainEvents.ResetDelayedEventsStorage();

        var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();
        var rebusUnitOfWork = new RebusUnitOfWork(
            unitOfWorkFactory: unitOfWorkFactory,
            isolationLevel: IsolationLevel.ReadCommitted
        );
        _fakeMessageContext = new FakeMessageContext();
        _unitOfWork = await rebusUnitOfWork.CreateAsync(_fakeMessageContext);

        try
        {
            _simulateApplicationTransactionWhichThrowsAnException();
        }
        catch
        {
            await rebusUnitOfWork.RollbackAsync(_fakeMessageContext, _unitOfWork);
            await rebusUnitOfWork.CleanupAsync(_fakeMessageContext, _unitOfWork);
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
#endif