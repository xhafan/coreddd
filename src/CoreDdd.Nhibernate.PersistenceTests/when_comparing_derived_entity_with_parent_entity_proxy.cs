using System;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.TestHelpers;
using CoreIoC;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.PersistenceTests
{
    [TestFixture]
    public class when_comparing_derived_entity_with_parent_entity_proxy : BasePersistenceTest
    {
        [SetUp]
        public void SetUp()
        {
            CreateDatabase();

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

            Save(derivedEntity);
            Clear();

            var parentEntityProxy = Load<TestEntityOne>(derivedEntity.Id);

            (derivedEntity == parentEntityProxy).ShouldBeTrue();
        }
    }
}