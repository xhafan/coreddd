using System.Collections.Generic;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.Queries;
using CoreIoC;

namespace CoreDdd.Nhibernate.Tests.Queries
{
    public class GetTestEntityCountTestAdoNetQueryHandler : IQueryHandler<GetTestEntityCountTestAdoNetQuery>
    {
        public IEnumerable<TResult> Execute<TResult>(GetTestEntityCountTestAdoNetQuery query)
        {
            var connection = IoC.Resolve<NhibernateUnitOfWork>().Session.Connection;
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "select count(Id) from TestEntity";
                return new[] { (TResult)cmd.ExecuteScalar() };
            }
        }
    }
}