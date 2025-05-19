using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Conventions
{
    [TestFixture]
    public class when_persisting_dto: BasePersistenceTest
    {
        [Test]
        public void dto_is_persisted()
        {
            var dto = new TestDto { Name = "abc" };

            UnitOfWork.Session.Save(dto);
            UnitOfWork.Clear();
            dto = UnitOfWork.Session.Load<TestDto>(dto.Id);

            dto.Name.ShouldBe("abc");
        }

        [Test]
        public void loaded_dto_is_readonly()
        {
            var dto = new TestDto { Name = "abc" };

            UnitOfWork.Session.Save(dto);
            UnitOfWork.Clear();
            dto = UnitOfWork.Session.Load<TestDto>(dto.Id);

            dto.Name = "xyz!";
            UnitOfWork.Session.Flush();
            UnitOfWork.Clear();
            dto = UnitOfWork.Session.Load<TestDto>(dto.Id);

            dto.Name.ShouldBe("abc");
        }
    }
}
