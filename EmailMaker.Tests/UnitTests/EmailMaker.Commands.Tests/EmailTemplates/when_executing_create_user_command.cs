using Core.Domain;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Users;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Commands.Tests.EmailTemplates
{
    [TestFixture]
    public class when_executing_create_user_command
    {
        private IRepository<User> _userRepository;
        private string _emailAddress = "email address";
        private string _password = "password";

        [SetUp]
        public void Context()
        {
            _userRepository = MockRepository.GenerateMock<IRepository<User>>();

            var handler = new CreateUserCommandHandler(_userRepository);
            handler.Execute(new CreateUserCommand { EmailAddress = _emailAddress, Password = _password});
        }

        [Test]
        public void user_was_created()
        {
            _userRepository.AssertWasCalled(a => a.Save(Arg<User>.Matches(p => p.EmailAddress == _emailAddress && p.Password == _password)));
        }

    }
}