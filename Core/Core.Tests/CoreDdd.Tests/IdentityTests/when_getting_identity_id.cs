using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.IdentityTests
{
    [TestFixture]
    public class when_getting_identity_id
    {
        private int _id;

        [SetUp]
        public void Context()
        {
            var entity = new Entity(12);
            _id = entity.Id;
        }

        [Test]
        public void retrived_id_is_correct()
        {
            _id.ShouldBe(12);
        }
    }
}
