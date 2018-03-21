using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.PersistenceTests.Conventions
{
    [TestFixture]
    public class when_persisting_long_text : BasePersistenceTest
    {
        [Test]
        public void long_text_is_persisted()
        {
            var longText = new string(Enumerable.Repeat('X', 20000).ToArray());
            var entity = new EntityWithText(longText);

            Save(entity);
            Clear();

            entity = Get<EntityWithText>(entity.Id);

            entity.Text.ShouldBe(longText);
        }
    }
}