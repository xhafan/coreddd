using System;
using System.Collections.Generic;
using CoreDdd.Nhibernate.Queries;
using CoreDdd.Nhibernate.UnitOfWorks;
#if NET8_0_OR_GREATER
using NHibernate;
#endif

#if !NET40
using System.Data.Common;
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.Tests.Queries;

public class GetTestEntityCountTestAdoNetQueryHandler(NhibernateUnitOfWork unitOfWork)
    : BaseAdoNetQueryHandler<GetTestEntityCountTestAdoNetQuery>(unitOfWork)
{
    public override IEnumerable<TResult> Execute<TResult>(GetTestEntityCountTestAdoNetQuery query)
    {
        using (var cmd = Connection.CreateCommand())
        {
            cmd.CommandText = "select count(\"Id\") from \"TestEntity\"";
#if NET40 || NET45 || NET461
            Session.Transaction.Enlist(cmd);
#else
                Session.GetCurrentTransaction().Enlist(cmd);
#endif

            var result = cmd.ExecuteScalar();
            return [(TResult)(object)Convert.ToInt32(result)];
        }
    }
        
    public override TResult ExecuteSingle<TResult>(GetTestEntityCountTestAdoNetQuery query)
    {
        using (var cmd = Connection.CreateCommand())
        {
            cmd.CommandText = "select count(\"Id\") from \"TestEntity\"";
#if NET40 || NET45 || NET461
            Session.Transaction.Enlist(cmd);
#else
                Session.GetCurrentTransaction().Enlist(cmd);
#endif

            var result = cmd.ExecuteScalar();
            return (TResult)(object)Convert.ToInt32(result);
        }
    }        

#if !NET40
    public override async Task<IEnumerable<TResult>> ExecuteAsync<TResult>(GetTestEntityCountTestAdoNetQuery query)
    {
        using (var cmd = Connection.CreateCommand())
        {
            cmd.CommandText = "select count(\"Id\") from \"TestEntity\"";
#if NET40 || NET45 || NET461
            Session.Transaction.Enlist(cmd);
#else
                Session.GetCurrentTransaction().Enlist(cmd);
#endif
            var result = await ((DbCommand)cmd).ExecuteScalarAsync();
            return [(TResult)(object)Convert.ToInt32(result)];
        }
    }
#endif
        
#if !NET40
    public override async Task<TResult> ExecuteSingleAsync<TResult>(GetTestEntityCountTestAdoNetQuery query)
    {
        using (var cmd = Connection.CreateCommand())
        {
            cmd.CommandText = "select count(\"Id\") from \"TestEntity\"";
#if NET40 || NET45 || NET461
            Session.Transaction.Enlist(cmd);
#else
                Session.GetCurrentTransaction().Enlist(cmd);
#endif
            var result = await ((DbCommand)cmd).ExecuteScalarAsync();
            return (TResult)(object)Convert.ToInt32(result);
        }
    }
#endif        
}