using NHibernate;
using NHibernate.Cfg;

namespace CoreDdd.Nhibernate.Configurations
{
    /// <summary>
    /// Represents NHibernate configuration.
    /// Derive your custom NHibernate configuration from <see cref="BaseNhibernateConfigurator"/>.
    /// </summary>
    public interface INhibernateConfigurator
    {
        /// <summary>
        /// Gets NHibernate session factory.
        /// </summary>
        /// <returns>NHibernate session factory</returns>
        ISessionFactory GetSessionFactory();

        /// <summary>
        /// Gets NHibernate configuration.
        /// </summary>
        /// <returns>NHibernate configuration</returns>
        Configuration GetConfiguration();
    }    
}