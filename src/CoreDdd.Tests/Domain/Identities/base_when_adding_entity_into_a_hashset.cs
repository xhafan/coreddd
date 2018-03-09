using System.Collections.Generic;
using CoreTest.Extensions;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Domain.Identities
{
    public abstract class base_when_adding_entity_into_a_hashset<TId>
    {
        private TestEntity<TId> _transientEntityOne;
        private HashSet<TestEntity<TId>> _entities;

        protected abstract TId GetId();

        [SetUp]
        public void Context()
        {
            _transientEntityOne = new TestEntity<TId>();
            _entities = new HashSet<TestEntity<TId>>(new[] { _transientEntityOne });
        }

        [Test]
        public void hashset_contains_transient_entity_()
        {
            _entities.Contains(_transientEntityOne).ShouldBe(true);
        }

        [Test]
        public void change_transient_entity_to_persitent_entity_and_check_hashset()
        {
            _transientEntityOne.SetPrivateProperty(x => x.Id, GetId());
            _entities.Contains(_transientEntityOne).ShouldBe(true);
        }

    }
}