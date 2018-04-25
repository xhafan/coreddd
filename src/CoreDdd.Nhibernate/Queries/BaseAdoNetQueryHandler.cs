using IQuery = CoreDdd.Queries.IQuery;

#if NET40 || NET45
using System.Data;
#else
using System.Data.Common;
#endif

#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.Queries
{
    public abstract class BaseAdoNetQueryHandler<TQuery> : BaseNhibernateQueryHandler<TQuery> where TQuery : IQuery
    {
#if NET40 || NET45
        protected IDbConnection Connection;
#else
        protected DbConnection Connection;
#endif

        protected BaseAdoNetQueryHandler()
        {
            Connection = Session.Connection;
        }
    }
}