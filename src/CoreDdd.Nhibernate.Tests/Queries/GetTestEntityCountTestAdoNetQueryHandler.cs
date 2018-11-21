using System;
using System.Collections.Generic;
using CoreDdd.Nhibernate.Queries;
using CoreDdd.Nhibernate.UnitOfWorks;
#if !NET40
using System.Data.Common;
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.Tests.Queries
{
    public class GetTestEntityCountTestAdoNetQueryHandler : BaseAdoNetQueryHandler<GetTestEntityCountTestAdoNetQuery>
    {
        public GetTestEntityCountTestAdoNetQueryHandler(NhibernateUnitOfWork unitOfWork)
            : base(unitOfWork)
        {            
        }

        public override IEnumerable<TResult> Execute<TResult>(GetTestEntityCountTestAdoNetQuery query)
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = "select count(\"Id\") from \"TestEntity\"";
                Session.Transaction.Enlist(cmd);

                var result = cmd.ExecuteScalar();
                return new[] { (TResult)(object)Convert.ToInt32(result) };
            }
        }

#if !NET40
        public override async Task<IEnumerable<TResult>> ExecuteAsync<TResult>(GetTestEntityCountTestAdoNetQuery query)
        {
            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = "select count(\"Id\") from \"TestEntity\"";
                Session.Transaction.Enlist(cmd);
                var result = await ((DbCommand)cmd).ExecuteScalarAsync();
                return new[] { (TResult)(object)Convert.ToInt32(result) };
            }
        }
#endif
    }
}
