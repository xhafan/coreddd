using CoreDdd.Domain.Repositories;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Users;
using FakeItEasy;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Commands.EmailTemplates
{
    [TestFixture]
    public class when_executing_create_user_command
    {
        private IRepository<User> _userRepository;
        private const string EmailAddress = "email address";
        private const string Password = "password";

        [SetUp]
        public void Context()
        {
            _userRepository = A.Fake<IRepository<User>>();

            var handler = new CreateUserCommandHandler(_userRepository);
            handler.Execute(new CreateUserCommand { EmailAddress = EmailAddress, Password = Password});
        }

        [Test]
        public void user_was_created()
        {
            A.CallTo(() => _userRepository.Save(A<User>.That.Matches(p => p.EmailAddress == EmailAddress && p.Password == Password))).MustHaveHappened();
        }

    }
}