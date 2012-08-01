using Core.Infrastructure;
using NHibernate;

namespace Core.Domain.Repositories
{
    public class NhibernateRepository<T> : NhibernateRepository<T, int>, IRepository<T> where T : IAggregateRoot<int>
    {
        public NhibernateRepository()
        {
            Session = UnitOfWork.CurrentSession;
        }
        
        public NhibernateRepository(ISession session)
            : base(session)
        {
        }
    }
}
