using System;
using System.Linq;
using CoreUtils.AmbientStorages;
using CoreUtils.Extensions;

namespace CoreDdd.Domain.Events
{
    /// <summary>
    /// Allows to raise a domain event from domain entities. To raise a domain event, call RaiseEvent() method.
    /// </summary>
    public static class DomainEvents // todo: implement async support for event domain handling?
    {
        private static IDomainEventHandlerFactory _domainEventHandlerFactory;
        private static readonly AmbientStorage<DelayedDomainEventHandlingItems> DelayedDomainEventHandlingItemsStorage =
            new AmbientStorage<DelayedDomainEventHandlingItems>();

        private static bool _isDelayedDomainEventHandlingEnabled;

        /// <summary>
        /// Initialize domain events with domain event handler factory and a flag whether to handle domain
        /// events immediately when raised (the default) or later (see <see cref="RaiseDelayedEvents"/>).
        /// </summary>
        /// <param name="domainEventHandlerFactory">A factory to instantiate domain event handlers</param>
        /// <param name="isDelayedDomainEventHandlingEnabled">false - immediate handling of domain events when raised, true - delayed handling of domain events</param>
        public static void Initialize(
            IDomainEventHandlerFactory domainEventHandlerFactory,
            bool isDelayedDomainEventHandlingEnabled = false
            )
        {
            _domainEventHandlerFactory = domainEventHandlerFactory;
            _isDelayedDomainEventHandlingEnabled = isDelayedDomainEventHandlingEnabled;
        }

        /// <summary>
        /// Resets the domain event storage used for delayed domain event handling to remove previous domain events.
        /// Usually called at the beginning of a new request/transaction.
        /// </summary>
        public static void ResetDelayedEventsStorage()
        {
            if (DelayedDomainEventHandlingItemsStorage.Value == null)
            {
                DelayedDomainEventHandlingItemsStorage.Value = new DelayedDomainEventHandlingItems();
            }
        }
        
        /// <summary>
        /// Raises a new domain event. Ideally call it within a domain entity code to express that a certain
        /// domain behavior has happened.
        /// </summary>
        /// <typeparam name="TDomainEvent">A domain event type</typeparam>
        /// <param name="domainEvent">A domain event instance</param>
        public static void RaiseEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            _CheckWasInitialized();

            if (_isDelayedDomainEventHandlingEnabled)
            {
                _RegisterDelayedEvent(domainEvent);
            }
            else
            {
                _raiseEventNow();
            }

            void _raiseEventNow()
            {

                var domainEventHandlers = _domainEventHandlerFactory.Create<TDomainEvent>().ToList();

                try
                {
                    domainEventHandlers.Each(domainEventHandler => domainEventHandler.Handle(domainEvent));
                }
                finally
                {
                    domainEventHandlers.Each(_domainEventHandlerFactory.Release);
                }
            }
        }

        private static void _CheckWasInitialized()
        {
            if (_domainEventHandlerFactory == null)
            {
                throw new InvalidOperationException(
                    "The domain events have not been initialized! Please call DomainEvents.Initialize(...) before using it.");
            }
        }

        private static void _RegisterDelayedEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            var delayedDomainEventHandlingItems = DelayedDomainEventHandlingItemsStorage.Value;
            if (delayedDomainEventHandlingItems == null)
            {
                throw new InvalidOperationException(
                    "DelayedDomainEventHandlingItems is null. Did you forget to call DomainEvents.ResetDelayedEventsStorage() ?");
            }

            var domainEventHandlers = _domainEventHandlerFactory.Create<TDomainEvent>();
            domainEventHandlers.Each(domainEventHandler =>
            {
                var delayedDomainEventHandlingItem = new DelayedDomainEventHandlingItem(domainEventHandler, () => domainEventHandler.Handle(domainEvent));
                delayedDomainEventHandlingItems.Enqueue(delayedDomainEventHandlingItem);
            });
        }

        /// <summary>
        /// Raises delayed domain events. When delayed domain event handling is used (see <see cref="Initialize"/>), 
        /// domain event handlers are not executed immediately when a domain event is raised, but are added into a queue,
        /// and raised by calling this method. This method is usually called after the main request/transaction completes.
        /// </summary>
        public static void RaiseDelayedEvents()
        {
            var delayedDomainEventHandlingItems = DelayedDomainEventHandlingItemsStorage.Value;
            if (delayedDomainEventHandlingItems == null) throw new InvalidOperationException("DelayedDomainEventHandlingItems is null.");

            if (delayedDomainEventHandlingItems.IsEmpty()) return;

            _ExecuteAllDomainEventHandlers(delayedDomainEventHandlingItems);
        }

        private static void _ExecuteAllDomainEventHandlers(DelayedDomainEventHandlingItems delayedDomainEventHandlingItems)
        {
            try
            {
                while (delayedDomainEventHandlingItems.Any())
                {
                    _executeOneHandler();
                }
            }
            finally
            {
                delayedDomainEventHandlingItems.Each(x => _domainEventHandlerFactory.Release(x.DomainEventHandler));
            }

            void _executeOneHandler()
            {
                var delayedDomainEventHandlingItem = delayedDomainEventHandlingItems.Dequeue();

                try
                {
                    delayedDomainEventHandlingItem.DomainEventHandlingAction();
                }
                finally
                {
                    _domainEventHandlerFactory.Release(delayedDomainEventHandlingItem.DomainEventHandler);
                }
            }
        }
    }
}
