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

        protected BaseNhibernateQueryHandler(NhibernateUnitOfWork unitOfWork)
        {
            Session = unitOfWork.Session;
        }

        public virtual IEnumerable<TResult> Execute<TResult>(TQuery query)
        {
            return Enumerable.Empty<TResult>();
        }

#if !NET40
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task<IEnumerable<TResult>> ExecuteAsync<TResult>(TQuery query)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return Enumerable.Empty<TResult>();
        }
#endif
    }
}