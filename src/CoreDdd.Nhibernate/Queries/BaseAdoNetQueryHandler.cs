using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.Queries;

#if NET40 || NET45
using System.Data;
#else
using System.Data.Common;
#endif

namespace CoreDdd.Nhibernate.Queries
{
    /// <summary>
    /// Base query handler for ADO.NET SQL queries.
    /// Derive your query handler from this base class when NHibernate QueryOver queries are not sufficient, 
    /// and you need to do SQL queries.
    /// </summary>
    /// <typeparam name="TQuery">A query type</typeparam>
    /// <typeparam name="TResult">A query result type</typeparam>
    public abstract class BaseAdoNetQueryHandler<TQuery, TResult> : BaseNhibernateQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Database connection taken from NHibernate session.
        /// </summary>
#if NET40 || NET45
        protected IDbConnection Connection;
#else
        protected DbConnection Connection;
#endif

        /// <summary>
        /// Initialized the instance.
        /// </summary>
        /// <param name="unitOfWork">An instance of NHibernate unit of work</param>
        protected BaseAdoNetQueryHandler(NhibernateUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            Connection = Session.Connection;
        }
    }
}