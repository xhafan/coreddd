using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Releasers;
using Castle.Windsor;
using Castle.MicroKernel;
using Castle.Core;
using Castle.MicroKernel.Registration;
using NServiceBus.ObjectBuilder.Common;

namespace NServiceBus.ObjectBuilder.CastleWindsor25
{

    /// <summary>
    ///Castle Windsor implementaton of IContainer.
    /// http://blog.paulhadfield.net/2010/09/updating-nservicebus-to-use-another.html
   /// </summary>
   public class Windsor25ObjectBuilder: IContainer
   {
        /// <summary>
        ///The container itself.
       /// </summary>
       public IWindsorContainer Container { get; set; }

        /// <summary>
        ///Instantites the class with a new WindsorContainer setting the NoTrackingReleasePolicy.
       /// </summary>
       public Windsor25ObjectBuilder()
        {
            Container = new WindsorContainer();
            Container.Kernel.ReleasePolicy = new NoTrackingReleasePolicy();
        }

        /// <summary>
        ///Instantiates the class saving the given container.
       /// </summary>
        /// <param name="container"></param>
       public Windsor25ObjectBuilder(IWindsorContainer container)
        {
            Container = container;
        }

        void IContainer.Configure(Type concreteComponent, ComponentCallModelEnum callModel)
        {
            var handler = GetHandlerForType(concreteComponent);
            if(handler == null)
            {
                var lifestyle = GetLifestyleTypeFrom(callModel);

                var reg = Component.For(GetAllServiceTypesFor(concreteComponent)).ImplementedBy(concreteComponent);
                reg.LifeStyle.Is(lifestyle);

                Container.Kernel.Register(reg);
            }
        }

        void IContainer.ConfigureProperty(Type component, string property, object value)
        {
            var handler = GetHandlerForType(component);
            if(handler == null)
                throw new InvalidOperationException("Cannot configure property for a type which hadn't been configured yet. Please call 'Configure' first.");

            handler.AddCustomDependencyValue(property, value);
        }

        void IContainer.RegisterSingleton(Type lookupType, object instance)
        {
            //Container.Kernel.AddComponentInstance(Guid.NewGuid().ToString(), lookupType, instance);
           Container.Register(Component.For(lookupType).Named(Guid.NewGuid().ToString()).Instance(instance));
        }

        object IContainer.Build(Type typeToBuild)
        {
            try
           {
                return Container.Resolve(typeToBuild);
            }
            catch(ComponentNotFoundException)
            {
                return null;
            }
        }

        IEnumerable<object> IContainer.BuildAll(Type typeToBuild)
        {
            return Container.ResolveAll(typeToBuild).Cast<object>();
        }

        private static LifestyleType GetLifestyleTypeFrom(ComponentCallModelEnum callModel)
        {
            switch(callModel)
            {
                case ComponentCallModelEnum.Singlecall: return LifestyleType.Transient;
                case ComponentCallModelEnum.Singleton: return LifestyleType.Singleton;
            }

            return LifestyleType.Undefined;
        }

        private static IEnumerable<Type> GetAllServiceTypesFor(Type t)
        {
            if(t == null)
                return new List<Type>();

            var result = new List<Type>(t.GetInterfaces()) { t };

            foreach (var interfaceType in t.GetInterfaces())
                result.AddRange(GetAllServiceTypesFor(interfaceType));

            return result;
        }

        private IHandler GetHandlerForType(Type concreteComponent)
        {
            return Container.Kernel.GetAssignableHandlers(typeof(object))
                .Where(h => h.ComponentModel.Implementation == concreteComponent)
                .FirstOrDefault();
        }
    }
}