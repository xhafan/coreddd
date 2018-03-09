using EmailMaker.Domain.Users;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Users
{
    [TestFixture]
    public class when_changing_password
    {
        private const string OldPassword = "old password";
        private const string NewPassword = "new password";
        private User _user;

        [SetUp]
        public void Context()
        {
            _user = UserBuilder.New
                .WithPassword(OldPassword)
                .Build();
            _user.ChangePassword(OldPassword, NewPassword);
        }

        [Test]
        public void password_was_changed()
        {
            _user.Password.ShouldBe(NewPassword);
        }
    }

}
