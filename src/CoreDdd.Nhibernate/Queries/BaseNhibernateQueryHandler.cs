using System.Collections.Generic;
using System.Linq;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.Queries;
using CoreIoC;
using NHibernate;
using IQuery = CoreDdd.Queries.IQuery;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.Queries
{
    public abstract class BaseNhibernateQueryHandler<TQuery> : IQueryHandler<TQuery> where TQuery : IQuery
    {
        protected ISession Session;

        protected BaseNhibernateQueryHandler()
        {
            Session = IoC.Resolve<NhibernateUnitOfWork>().Session;
        }

        public virtual IEnumerable<TResult> Execute<TResult>(TQuery query)
        {
            return Enumerable.Empty<TResult>();
        }

#if !NET40
        public async virtual Task<IEnumerable<TResult>> ExecuteAsync<TResult>(TQuery query)
        {
            return Enumerable.Empty<TResult>();
        }
#endif
    }
}