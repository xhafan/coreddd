﻿#if !NET451 
using System;
using System.Threading.Tasks;
using System.Transactions;
using CoreDdd.Domain.Events;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.TestHelpers.DomainEvents;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using IntegrationTestsShared.TransactionScopes;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Rebus.UnitOfWork.Tests.RebusTransactionScopeUnitOfWorks;

[TestFixture]
public class when_saving_entity_and_throwing_exception_within_rebus_transaction_scope_unit_of_work_async
{
    private VolatileResourceManager _volatileResourceManager;
    private IRepository<TestEntityWithDomainEvent> _entityRepository;
    private TestEntityWithDomainEvent _entity;
    private TestDomainEvent _raisedDomainEvent;
    private FakeMessageContext _fakeMessageContext;
    private (TransactionScope transactionScope, IUnitOfWork unitOfWork) _transactionScopeUnitOfWork;

    [SetUp]
    public async Task Context()
    {
        var domainEventHandlerFactory = new FakeDomainEventHandlerFactory(domainEvent => _raisedDomainEvent = (TestDomainEvent)domainEvent);
        DomainEvents.Initialize(domainEventHandlerFactory);

        _volatileResourceManager = new VolatileResourceManager();

        var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();
        var rebusTransactionScopeUnitOfWork = new RebusTransactionScopeUnitOfWork(
            unitOfWorkFactory: unitOfWorkFactory,
            isolationLevel: IsolationLevel.ReadCommitted,
            transactionScopeEnlistmentAction: transactionScope => _volatileResourceManager.EnlistIntoTransactionScope(transactionScope)
        );
        _fakeMessageContext = new FakeMessageContext();
        _transactionScopeUnitOfWork = await rebusTransactionScopeUnitOfWork.CreateAsync(_fakeMessageContext);

        try
        {
            _simulateApplicationTransactionWhichThrowsAnException();
        }
        catch
        {
            await rebusTransactionScopeUnitOfWork.RollbackAsync(_fakeMessageContext, _transactionScopeUnitOfWork);
            await rebusTransactionScopeUnitOfWork.CleanupAsync(_fakeMessageContext, _transactionScopeUnitOfWork);
        }
    }

    private void _simulateApplicationTransactionWhichThrowsAnException()
    {
        _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();

        _entity = new TestEntityWithDomainEvent();
        _entity.BehaviouralMethodWithRaisingDomainEvent();

        _entityRepository.Save(_entity);

        _volatileResourceManager.SetMemberValue(23);

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
    public void volatile_resource_manager_value_is_not_set_after_transaction_scope_rollback()
    {
        _volatileResourceManager.MemberValue.ShouldNotBe(23);
    }

    [Test]
    public void domain_event_is_still_handled_even_though_the_transaction_is_rolled_back()
    {
        // the domain event handler is executed, but developer needs to make sure that domain event handler execution results 
        // are rolled back if the transaction scope is rolled back. For instance when sending a bus message from the domain event handler
        // the message should not be sent when the transaction scope rolls back.
        _raisedDomainEvent.ShouldNotBeNull();
    }
}
#endif