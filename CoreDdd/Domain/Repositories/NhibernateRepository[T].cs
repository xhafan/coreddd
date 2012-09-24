using CoreDdd.Infrastructure;
using NHibernate;

namespace CoreDdd.Domain.Repositories
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
