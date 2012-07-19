using NHibernate;
using NHibernate.Cfg;

namespace Core.Infrastructure
{
    public interface INhibernateConfigurator
    {
        ISessionFactory GetSessionFactory();
        Configuration GetConfiguration();
    }    
}