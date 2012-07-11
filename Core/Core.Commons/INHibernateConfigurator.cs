using NHibernate;
using NHibernate.Cfg;

namespace Core.Commons
{
    public interface INHibernateConfigurator
    {
        ISessionFactory GetSessionFactory();
        Configuration GetConfiguration();
    }    
}