using Core.Utilities;
using EmailMaker.TestHelper.Builders;
using EmailMaker.Utilities;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.UserTests
{
    [TestFixture]
    public class when_changing_password_with_incorrect_old_password
    {
        private CoreException _exception;

        [SetUp]
        public void Context()
        {
            var user = UserBuilder.New.Build();
            _exception = Assert.Throws<CoreException>(() => user.ChangePassword("incorrect old password", "new password"));
        }

        [Test]
        public void password_was_changed()
        {
            _exception.Message.ShouldBe("Old password doesnot match");
        }
    }
}