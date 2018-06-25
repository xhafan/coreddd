using System;
using System.Linq;
using CoreIoC;
using CoreUtils.Extensions;
using CoreUtils.Storages;

namespace CoreDdd.Domain.Events
{
    public static class DomainEvents
    {
        private static bool _isDelayedDomainEventHandlingEnabled;

        public static void EnableDelayedDomainEventHandling()
        {
            _isDelayedDomainEventHandlingEnabled = true;
        }

        public static void DisableDelayedDomainEventHandling()
        {
            _isDelayedDomainEventHandlingEnabled = false;
        }

        public static void RaiseEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            if (_isDelayedDomainEventHandlingEnabled)
            {
                RegisterDelayedEvent(domainEvent);
            }
            else
            {
                _raiseEventNow();
            }

            void _raiseEventNow()
            {
                var domainEventHandlers = IoC.ResolveAll<IDomainEventHandler<TDomainEvent>>().ToList();

                try
                {
                    domainEventHandlers.Each(domainEventHandler => domainEventHandler.Handle(domainEvent));
                }
                finally
                {
                    domainEventHandlers.Each(IoC.Release);
                }
            }
        }


        private static void RegisterDelayedEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            var delayedDomainEventHandlingItems = _getDelayedDomainEventHandlingItems();

            var domainEventHandlers = IoC.ResolveAll<IDomainEventHandler<TDomainEvent>>();
            domainEventHandlers.Each(domainEventHandler =>
            {
                var delayedDomainEventHandlingItem = new DelayedDomainEventHandlingItem(domainEventHandler, () => domainEventHandler.Handle(domainEvent));
                delayedDomainEventHandlingItems.Enqueue(delayedDomainEventHandlingItem);
            });

            DelayedDomainEventHandlingItems _getDelayedDomainEventHandlingItems()
            {
                var delayedDomainEventHandlingItemsStorage = IoC.Resolve<IStorage<DelayedDomainEventHandlingItems>>();
                if (delayedDomainEventHandlingItemsStorage.Get() == null)
                {
                    delayedDomainEventHandlingItemsStorage.Set(new DelayedDomainEventHandlingItems());
                }

                return delayedDomainEventHandlingItemsStorage.Get();
            }
        }

        public static void RaiseDelayedEvents(Action<Action> eventHandlingSurroundingAction) // todo: try to make this async? test in both asp.net and asp.net core
        {
            var delayedDomainEventHandlingItemsStorage = IoC.Resolve<IStorage<DelayedDomainEventHandlingItems>>();

            try
            {
                var delayedDomainEventHandlingItems = delayedDomainEventHandlingItemsStorage.Get();
                if (delayedDomainEventHandlingItems == null) return;

                _ExecuteAllDomainEventHandlers(eventHandlingSurroundingAction, delayedDomainEventHandlingItems);
            }
            finally
            {
                IoC.Release(delayedDomainEventHandlingItemsStorage);
            }
        }

        private static void _ExecuteAllDomainEventHandlers(
            Action<Action> eventHandlingSurroundingAction,
            DelayedDomainEventHandlingItems delayedDomainEventHandlingItems
            )
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
                delayedDomainEventHandlingItems.Each(x => IoC.Release(x.DomainEventHandler));
            }

            void _executeOneHandler()
            {
                var delayedDomainEventHandlingItem = delayedDomainEventHandlingItems.Dequeue();

                try
                {
                    eventHandlingSurroundingAction(delayedDomainEventHandlingItem.DomainEventHandlingAction);
                }
                finally
                {
                    IoC.Release(delayedDomainEventHandlingItem.DomainEventHandler);
                }
            }

        }
    }
}
