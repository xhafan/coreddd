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
                var domainEventHandlers = IoC.ResolveAll<IDomainEventHandler<TDomainEvent>>();
                domainEventHandlers.Each(x => x.Handle(domainEvent));
            }
        }

        private static void RegisterDelayedEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            var delayedDomainEventHandlingActions = _getDelayedDomainEventHandlingActions();

            var domainEventHandlers = IoC.ResolveAll<IDomainEventHandler<TDomainEvent>>();
            domainEventHandlers.Each(x => delayedDomainEventHandlingActions.Enqueue(() => x.Handle(domainEvent)));


            DelayedDomainEventHandlingActions _getDelayedDomainEventHandlingActions()
            {
                var delayedDomainEventHandlingActionsStorage = IoC.Resolve<IStorage<DelayedDomainEventHandlingActions>>();
                if (delayedDomainEventHandlingActionsStorage.Get() == null)
                {
                    delayedDomainEventHandlingActionsStorage.Set(new DelayedDomainEventHandlingActions());
                }

                return delayedDomainEventHandlingActionsStorage.Get();
            }
        }

        public static void RaiseDelayedEvents(Action<Action> eventHandlingSurroundingAction)
        {
            var delayedDomainEventHandlingActionsStorage = IoC.Resolve<IStorage<DelayedDomainEventHandlingActions>>();
            var delayedDomainEventHandlingActions = delayedDomainEventHandlingActionsStorage.Get();
            if (delayedDomainEventHandlingActions == null) return;

            while (delayedDomainEventHandlingActions.Any())
            {
                var delayedDomainEventHandlingAction = delayedDomainEventHandlingActions.Dequeue();
                eventHandlingSurroundingAction(delayedDomainEventHandlingAction);
            }

            delayedDomainEventHandlingActions.Clear();
        }
    }
}
