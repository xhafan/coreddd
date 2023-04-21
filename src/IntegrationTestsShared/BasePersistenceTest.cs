﻿using CoreDdd.Nhibernate.UnitOfWorks;
using NUnit.Framework;

namespace IntegrationTestsShared
{
    public abstract class BasePersistenceTest
    {
        protected NhibernateUnitOfWork UnitOfWork;

        protected BasePersistenceTest()
        {
            UnitOfWork = new NhibernateUnitOfWork(new TestNhibernateConfigurator());
        }

        [SetUp]
        public void TestFixtureSetUp()
        {
            UnitOfWork.BeginTransaction();
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
            try
            {
                UnitOfWork.Flush();
            }
            finally
            {
                UnitOfWork.Rollback();
            }
        }
    }
}