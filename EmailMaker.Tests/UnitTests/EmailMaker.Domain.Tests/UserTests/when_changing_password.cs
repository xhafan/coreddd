using EmailMaker.Domain.Users;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.UserTests
{
    [TestFixture]
    public class when_changing_password
    {
        private string _oldPassword = "old password";
        private string _newPassword = "new password";
        private User _user;

        [SetUp]
        public void Context()
        {
            _user = UserBuilder.New
                .WithPassword(_oldPassword)
                .Build();
            _user.ChangePassword(_oldPassword, _newPassword);
        }

        [Test]
        public void password_was_changed()
        {
            _user.Password.ShouldBe(_newPassword);
        }
    }

}
