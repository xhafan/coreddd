using Castle.DynamicProxy;
using CoreDdd.Domain;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Castle.Tests.EntityEquality
{
    [TestFixture]
    public class when_comparing_entity_with_entity_proxy
    {
        [Test]
        public void compare_entity_with_its_entity_proxy_with_the_same_id()
        {
            var testEntityOne = new TestEntityOne();
            testEntityOne.SetId(23);
            var testEntityOneProxy = new ProxyGenerator().CreateClassProxy<TestEntityOne>();
            testEntityOneProxy.SetId(23);

            Equals(testEntityOne, testEntityOneProxy).ShouldBeTrue();
            Equals(testEntityOneProxy, testEntityOne).ShouldBeTrue();
        }

        [Test]
        public void compare_entity_with_its_entity_proxy_with_different_id()
        {
            var testEntityOne = new TestEntityOne();
            testEntityOne.SetId(23);
            var testEntityOneProxy = new ProxyGenerator().CreateClassProxy<TestEntityOne>();
            testEntityOneProxy.SetId(24);

            Equals(testEntityOne, testEntityOneProxy).ShouldBeFalse();
            Equals(testEntityOneProxy, testEntityOne).ShouldBeFalse();
        }

        [Test]
        public void compare_entity_with_different_entity_proxy_with_the_same_id()
        {
            var testEntityOne = new TestEntityOne();
            testEntityOne.SetId(23);
            var testEntityTwoProxy = new ProxyGenerator().CreateClassProxy<TestEntityTwo>();
            testEntityTwoProxy.SetId(23);

            Equals(testEntityOne, testEntityTwoProxy).ShouldBeFalse();
            Equals(testEntityTwoProxy, testEntityOne).ShouldBeFalse();
        }

        [Test]
        public void compare_entity_with_different_entity_proxy_with_different_id()
        {
            var testEntityOne = new TestEntityOne();
            testEntityOne.SetId(23);
            var testEntityTwoProxy = new ProxyGenerator().CreateClassProxy<TestEntityTwo>();
            testEntityTwoProxy.SetId(24);

            Equals(testEntityOne, testEntityTwoProxy).ShouldBeFalse();
            Equals(testEntityTwoProxy, testEntityOne).ShouldBeFalse();
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
    }
}