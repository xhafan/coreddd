using EmailMaker.Domain.Users;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Users
{
    [TestFixture]
    public class when_creating_user
    {
        private const string FirstName = "first name";
        private const string LastName = "last name";
        private const string EmailAddress = "email address";
        private const string Password = "password"; 
        private User _user;

        [SetUp]
        public void Context()
        {
            _user = new User(FirstName, LastName, EmailAddress, Password);
        }

        [Test]
        public void password_was_changed()
        {
            _user.FirstName.ShouldBe(FirstName);
            _user.LastName.ShouldBe(LastName);
            _user.EmailAddress.ShouldBe(EmailAddress);
            _user.Password.ShouldBe(Password);
        }
    }
}