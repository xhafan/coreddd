using System.Collections.Generic;
using Core.Tests.Helpers.Extensions;
using NUnit.Framework;
using Shouldly;

namespace Core.Tests.Domain.IdentityTests
{
    [TestFixture]
    public class when_adding_entity_into_a_hashset
    {
        private Entity _transientEntityOne;
        private HashSet<Entity> _entities;

        [SetUp]
        public void Context()
        {
            _transientEntityOne = new Entity();
            _entities = new HashSet<Entity>(new[] { _transientEntityOne });
        }

        [Test]
        public void hashset_contains_transient_entity_()
        {
            _entities.Contains(_transientEntityOne).ShouldBe(true);
        }

        [Test]
        public void change_transient_entity_to_persitent_entity_and_check_hashset()
        {
            _transientEntityOne.SetPrivateProperty(x => x.Id, 23);
            _entities.Contains(_transientEntityOne).ShouldBe(true);
        }

    }
}