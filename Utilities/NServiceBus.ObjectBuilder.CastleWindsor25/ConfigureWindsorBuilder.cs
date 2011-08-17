using NServiceBus.ObjectBuilder.CastleWindsor25;

namespace NServiceBus
{
    using ObjectBuilder.Common.Config;
    using Castle.Windsor;    

    /// <summary>
    /// Contains extension methods to NServiceBus.Configure.
    /// </summary>
    public static class ConfigureWindsorBuilder
    {
        /// <summary>
        /// Use the Castle Windsor builder with the NoTrackingReleasePolicy.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static Configure CastleWindsor25Builder(this Configure config)
        {
            ConfigureCommon.With(config, new Windsor25ObjectBuilder());

            return config;
        }

        /// <summary>
        /// Use the Castle Windsor builder passing in a preconfigured container to be used by nServiceBus.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public static Configure CastleWindsor25Builder(this Configure config, IWindsorContainer container)
        {
            ConfigureCommon.With(config, new Windsor25ObjectBuilder(container));

            return config;
        }
    }
}