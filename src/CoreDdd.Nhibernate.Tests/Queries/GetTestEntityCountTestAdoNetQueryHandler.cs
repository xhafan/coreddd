using System;
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
            var session = IoC.Resolve<NhibernateUnitOfWork>().Session;
            var connection = session.Connection;
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "select count(Id) from TestEntity";
                session.Transaction.Enlist(cmd);

                object result = cmd.ExecuteScalar();
                return new[] { (TResult)(object)Convert.ToInt32(result) };
            }
        }
    }
}