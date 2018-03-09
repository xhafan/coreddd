using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_constructing : NhibernateUnitOfWorkSetup
    {
        [Test]
        public void session_is_not_opened()
        {
            UnitOfWork.Session.ShouldBe(null);
        }

        [Test]
        public void unit_of_work_is_not_active()
        {
            UnitOfWork.IsActive().ShouldBe(false);
        }
    }
}