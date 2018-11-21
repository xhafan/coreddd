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
        ISessionFactory GetSessionFactory();
        Configuration GetConfiguration();
    }    
}