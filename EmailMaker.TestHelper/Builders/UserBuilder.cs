using EmailMaker.Domain.Users;

namespace EmailMaker.TestHelper.Builders
{
    public class UserBuilder
    {
        private string _password = "password";

        public static UserBuilder New
        {
            get
            {
                return new UserBuilder();
            }
        }

        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public User Build()
        {
            var user = new User("firstname", "lastname", "email", _password);
            return user;
        }
    }
}