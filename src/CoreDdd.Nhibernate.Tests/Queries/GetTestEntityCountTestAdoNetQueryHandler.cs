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
    : BaseAdoNetQueryHandler<GetTestEntityCountTestAdoNetQuery, int>(unitOfWork)
{
    public override IEnumerable<int> Execute(GetTestEntityCountTestAdoNetQuery query)
    {
        using var cmd = Connection.CreateCommand();
        cmd.CommandText = "select count(\"Id\") from \"TestEntity\"";
#if NET40 || NET45 || NET461
        Session.Transaction.Enlist(cmd);
#else
        Session.GetCurrentTransaction().Enlist(cmd);
#endif

        var result = cmd.ExecuteScalar();
        return [Convert.ToInt32(result)];
    }
        
    public override int ExecuteSingle(GetTestEntityCountTestAdoNetQuery query)
    {
        using var cmd = Connection.CreateCommand();
        cmd.CommandText = "select count(\"Id\") from \"TestEntity\"";
#if NET40 || NET45 || NET461
        Session.Transaction.Enlist(cmd);
#else
        Session.GetCurrentTransaction().Enlist(cmd);
#endif

        var result = cmd.ExecuteScalar();
        return Convert.ToInt32(result);
    }        

#if !NET40
    public override async Task<IEnumerable<int>> ExecuteAsync(GetTestEntityCountTestAdoNetQuery query)
    {
        using var cmd = Connection.CreateCommand();
        cmd.CommandText = "select count(\"Id\") from \"TestEntity\"";
#if NET40 || NET45 || NET461
        Session.Transaction.Enlist(cmd);
#else
        Session.GetCurrentTransaction().Enlist(cmd);
#endif
        var result = await ((DbCommand)cmd).ExecuteScalarAsync();
        return [Convert.ToInt32(result)];
    }
#endif
        
#if !NET40
    public override async Task<int> ExecuteSingleAsync(GetTestEntityCountTestAdoNetQuery query)
    {
        using var cmd = Connection.CreateCommand();
        cmd.CommandText = "select count(\"Id\") from \"TestEntity\"";
#if NET40 || NET45 || NET461
        Session.Transaction.Enlist(cmd);
#else
        Session.GetCurrentTransaction().Enlist(cmd);
#endif
        var result = await ((DbCommand)cmd).ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }
#endif        
}