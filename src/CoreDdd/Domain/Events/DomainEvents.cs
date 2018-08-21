using System;
using System.Linq;
using CoreUtils.Extensions;
using CoreUtils.Storages;

namespace CoreDdd.Domain.Events
{
    public static class DomainEvents
    {
        private static IDomainEventHandlerFactory _domainEventHandlerFactory;
        private static IStorageFactory _storageFactory;

        private static bool _isDelayedDomainEventHandlingEnabled;

        public static void Initialize(IDomainEventHandlerFactory domainEventHandlerFactory)
        {
            _domainEventHandlerFactory = domainEventHandlerFactory;
            _isDelayedDomainEventHandlingEnabled = false;
        }

        public static void InitializeWithDelayedDomainEventHandling(
            IDomainEventHandlerFactory domainEventHandlerFactory,
            IStorageFactory storageFactory
        )
        {
            _domainEventHandlerFactory = domainEventHandlerFactory;
            _storageFactory = storageFactory;
            _isDelayedDomainEventHandlingEnabled = true;
        }

        public static void RaiseEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
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
                _CheckWasInitialized();

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

        private static void _CheckWasInitializedWithDelayedDomainEventHandling()
        {
            if (_domainEventHandlerFactory == null || _storageFactory == null)
            {
                throw new InvalidOperationException(
                    "The domain events have not been initialized! Please call DomainEvents.InitializeWithDelayedDomainEventHandling(...) before using it.");
            }
        }

        private static void _RegisterDelayedEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            _CheckWasInitializedWithDelayedDomainEventHandling();

            var delayedDomainEventHandlingItems = _getDelayedDomainEventHandlingItems();

            var domainEventHandlers = _domainEventHandlerFactory.Create<TDomainEvent>();
            domainEventHandlers.Each(domainEventHandler =>
            {
                var delayedDomainEventHandlingItem = new DelayedDomainEventHandlingItem(domainEventHandler, () => domainEventHandler.Handle(domainEvent));
                delayedDomainEventHandlingItems.Enqueue(delayedDomainEventHandlingItem);
            });

            DelayedDomainEventHandlingItems _getDelayedDomainEventHandlingItems()
            {
                var delayedDomainEventHandlingItemsStorage = _storageFactory.Create<DelayedDomainEventHandlingItems>();
                if (delayedDomainEventHandlingItemsStorage.Get() == null)
                {
                    delayedDomainEventHandlingItemsStorage.Set(new DelayedDomainEventHandlingItems());
                }

                return delayedDomainEventHandlingItemsStorage.Get();
            }
        }

        public static void RaiseDelayedEvents()
        {
            var delayedDomainEventHandlingItemsStorage = _storageFactory.Create<DelayedDomainEventHandlingItems>();

            try
            {
                var delayedDomainEventHandlingItems = delayedDomainEventHandlingItemsStorage.Get();
                if (delayedDomainEventHandlingItems == null) return;

                _ExecuteAllDomainEventHandlers(delayedDomainEventHandlingItems);
            }
            finally
            {
                _storageFactory.Release(delayedDomainEventHandlingItemsStorage);
            }
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
