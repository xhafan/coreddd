using NHibernate;
using NHibernate.Cfg;

namespace CoreDdd.Nhibernate.Configurations
{
    public interface INhibernateConfigurator
    {
        ISessionFactory GetSessionFactory();
        Configuration GetConfiguration();
    }    
}