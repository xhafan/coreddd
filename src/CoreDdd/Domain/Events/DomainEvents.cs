using System;
using System.Linq;
using CoreUtils.AmbientStorages;
using CoreUtils.Extensions;

namespace CoreDdd.Domain.Events
{
    public static class DomainEvents
    {
        private static IDomainEventHandlerFactory _domainEventHandlerFactory;
        private static readonly AmbientStorage<DelayedDomainEventHandlingItems> DelayedDomainEventHandlingItemsStorage =
            new AmbientStorage<DelayedDomainEventHandlingItems>();

        private static bool _isDelayedDomainEventHandlingEnabled;

        public static void Initialize(
            IDomainEventHandlerFactory domainEventHandlerFactory,
            bool isDelayedDomainEventHandlingEnabled = false
            )
        {
            _domainEventHandlerFactory = domainEventHandlerFactory;
            _isDelayedDomainEventHandlingEnabled = isDelayedDomainEventHandlingEnabled;
        }

        public static void ResetDelayedEventsStorage()
        {
            DelayedDomainEventHandlingItemsStorage.Value = new DelayedDomainEventHandlingItems();
        }
        
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

            var domainEventHandlers = _domainEventHandlerFactory.Create<TDomainEvent>();
            domainEventHandlers.Each(domainEventHandler =>
            {
                var delayedDomainEventHandlingItem = new DelayedDomainEventHandlingItem(domainEventHandler, () => domainEventHandler.Handle(domainEvent));
                delayedDomainEventHandlingItems.Enqueue(delayedDomainEventHandlingItem);
            });
        }

        public static void RaiseDelayedEvents()
        {
            var delayedDomainEventHandlingItems = DelayedDomainEventHandlingItemsStorage.Value;
            if (delayedDomainEventHandlingItems == null) throw new InvalidOperationException("DelayedDomainEventHandlingItems is not set.");

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
