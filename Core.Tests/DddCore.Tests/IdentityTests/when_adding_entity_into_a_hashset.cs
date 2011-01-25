using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using TestHelper.Extensions;

namespace DddCore.Tests.IdentityTests
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
            _transientEntityOne.SetPrivateAttribute("_id", 23);
            _entities.Contains(_transientEntityOne).ShouldBe(true);
        }

    }
}