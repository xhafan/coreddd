using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Domain.Identities
{
    public abstract class base_when_getting_entity_id<TId>
    {
        private TId _id;

        protected abstract TId GetId();

        [SetUp]
        public void Context()
        {
            var entity = new TestEntity<TId>(GetId());
            _id = entity.Id;
        }

        [Test]
        public void retrived_id_is_correct()
        {
            _id.ShouldBe(GetId());
        }
    }
}