using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.Tests.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Conventions
{
    [TestFixture]
    public class when_persisting_long_text : BasePersistenceTest
    {
        [Test]
        public void long_text_is_persisted()
        {
            var longText = new string(Enumerable.Repeat('X', 20000).ToArray());
            var entity = new EntityWithText(longText);

            UnitOfWork.Save(entity);
            UnitOfWork.Clear();

            entity = UnitOfWork.Get<EntityWithText>(entity.Id);

            entity.Text.ShouldBe(longText);
        }
    }
}