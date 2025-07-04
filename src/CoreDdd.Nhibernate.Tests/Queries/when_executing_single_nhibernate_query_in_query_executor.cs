﻿using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Queries;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Queries;

[TestFixture]
public class when_executing_single_nhibernate_query_in_query_executor : BaseIoCPersistenceTest
{
    private int _result;
    private GetTestEntityCountTestNhibernateQuery _query;
      
    [SetUp]
    public void Context()
    {
        _persistTestEntity();
        _query = new GetTestEntityCountTestNhibernateQuery();

        var queryExecutor = IoC.Resolve<IQueryExecutor>();
        _result = queryExecutor.ExecuteSingle<GetTestEntityCountTestNhibernateQuery, int>(_query);

        void _persistTestEntity()
        {
            var testEntityOne = new TestEntity();
            UnitOfWork.Save(testEntityOne);
            var testEntityTwo = new TestEntity();
            UnitOfWork.Save(testEntityTwo);
        }
    }

    [Test]
    public void two_entities_are_counted()
    {
        _result.ShouldBe(2);
    }
    
}