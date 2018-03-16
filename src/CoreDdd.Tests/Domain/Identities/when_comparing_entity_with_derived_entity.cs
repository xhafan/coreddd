using CoreDdd.Domain;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Domain.Identities
{
    [TestFixture]
    public class when_comparing_entity_with_derived_entity
    {
        [Test]
        public void compare_entity_with_its_derived_entity_with_the_same_id()
        {
            var testEntityOne = new TestEntityOne();
            testEntityOne.SetId(23);
            var derivedTestEntityOne = new DerivedTestEntityOne();
            derivedTestEntityOne.SetId(23);

            Equals(testEntityOne, derivedTestEntityOne).ShouldBeTrue();
            Equals(derivedTestEntityOne, testEntityOne).ShouldBeTrue();
        }

        [Test]
        public void compare_entity_with_its_derived_entity_with_different_id()
        {
            var testEntityOne = new TestEntityOne();
            testEntityOne.SetId(23);
            var derivedTestEntityOne = new DerivedTestEntityOne();
            derivedTestEntityOne.SetId(24);

            Equals(testEntityOne, derivedTestEntityOne).ShouldBeFalse();
            Equals(derivedTestEntityOne, testEntityOne).ShouldBeFalse();
        }

        public class TestEntityOne : Entity
        {
            public void SetId(int id)
            {
                Id = id;
            }
        }

        public class TestEntityTwo : Entity
        {
            public void SetId(int id)
            {
                Id = id;
            }
        }

        public class DerivedTestEntityOne : TestEntityOne
        {
        }
    }
}