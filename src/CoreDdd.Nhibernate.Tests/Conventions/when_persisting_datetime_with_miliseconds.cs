using System;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.Tests.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Conventions
{
    [TestFixture]
    public class when_persisting_datetime_with_miliseconds : BasePersistenceTest
    {
        [Test]
        public void miliseconds_are_persisted()
        {
            var entity = new EntityWithDateTime(DateTime.Today.AddMilliseconds(123));

            Save(entity);
            Clear();

            entity = Get<EntityWithDateTime>(entity.Id);

            entity.DateTime.Millisecond.ShouldBe(123);
        }
    }
}