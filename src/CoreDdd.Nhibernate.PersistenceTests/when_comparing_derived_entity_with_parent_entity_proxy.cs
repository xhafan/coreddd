using System;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.TestHelpers;
using CoreIoC;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.PersistenceTests
{
    [TestFixture]
    public class when_comparing_derived_entity_with_parent_entity_proxy : BasePersistenceTest2
    {
        private IRepository<DerivedTestEntityOne> _derivedTestEntityOneRepository;
        private IRepository<TestEntityOne> _testEntityOneRepository;

        [SetUp]
        public void SetUp()
        {
            CreateDatabase();

            _derivedTestEntityOneRepository = new NhibernateRepository<DerivedTestEntityOne>(UnitOfWork);
            _testEntityOneRepository = new NhibernateRepository<TestEntityOne>(UnitOfWork);

            void CreateDatabase()
            {
                new SchemaExport(IoC.Resolve<INhibernateConfigurator>().GetConfiguration()).Execute(
                        useStdOut: true,
                        execute: true,
                        justDrop: false,
                        connection: UnitOfWork.Session.Connection,
                        exportOutput: Console.Out)
                    ;
            }
        }

        [Test]
        public void derived_entity_and_its_parent_entity_proxy_are_equal()
        {
            var derivedEntity = new DerivedTestEntityOne();

            _derivedTestEntityOneRepository.Save(derivedEntity);
            UnitOfWork.Flush();
            UnitOfWork.Clear();

            var parentEntityProxy = _testEntityOneRepository.Load(derivedEntity.Id);

            (derivedEntity == parentEntityProxy).ShouldBeTrue();
        }
    }
}