using NHibernate;
using NHibernate.Cfg;

namespace CoreDdd.Infrastructure
{
    public interface INhibernateConfigurator
    {
        ISessionFactory GetSessionFactory();
        Configuration GetConfiguration();
    }    
}