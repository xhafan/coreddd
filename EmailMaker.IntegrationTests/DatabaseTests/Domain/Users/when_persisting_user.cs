using EmailMaker.Domain.Users;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.Domain.Users
{
    [TestFixture]
    public class when_persisting_user : BaseEmailMakerSimplePersistenceTest
    {
        private const string FirstName = "first name";
        private const string LastName = "last name";
        private const string EmailAddress = "email address";
        private const string Password = "password";
        private User _user;
        private User _retrievedUser;

        protected override void PersistenceContext()
        {
            _user = new User(FirstName, LastName, EmailAddress, Password);
            Save(_user);
        }

        protected override void PersistenceQuery()
        {
            _retrievedUser = Get<User>(_user.Id);
        }

        [Test]
        public void user_correctly_retrieved()
        {
            _retrievedUser.FirstName.ShouldBe(FirstName);
            _retrievedUser.LastName.ShouldBe(LastName);
            _retrievedUser.EmailAddress.ShouldBe(EmailAddress);
            _retrievedUser.Password.ShouldBe(Password);
        }
    }
}
