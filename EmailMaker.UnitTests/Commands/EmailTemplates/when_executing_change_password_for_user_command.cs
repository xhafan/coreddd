using Core.Domain;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Users;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.UnitTests.Commands.EmailTemplates
{
    [TestFixture]
    public class when_executing_change_password_for_user_command
    {
        private IRepository<User> _userRepository;
        private int _userId = 23;
        private string _oldPassword = "old password";
        private string _newPassword = "new password";
        private User _user;

        [SetUp]
        public void Context()
        {
            _userRepository = MockRepository.GenerateStub<IRepository<User>>();
            _user = MockRepository.GenerateMock<User>();
            _userRepository.Stub(a => a.GetById(_userId)).Return(_user);
            var handler = new ChangePasswordForUserCommandHandler(_userRepository);
            handler.Execute(new ChangePasswordForUserCommand { UserId = _userId, OldPassword = _oldPassword, NewPassword = _newPassword});
        }

        [Test]
        public void password_was_changed()
        {
            _user.AssertWasCalled(a => a.ChangePassword(_oldPassword, _newPassword));
        }

    }
}