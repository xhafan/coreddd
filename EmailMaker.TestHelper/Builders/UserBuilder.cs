using EmailMaker.Domain.Users;

namespace EmailMaker.TestHelper.Builders
{
    public class UserBuilder
    {       

        public static UserBuilder New
        {
            get
            {
                return new UserBuilder();
            }
        }

        public User Build()
        {
            var user = new User("firstname", "lastname", "email", "password");
            return user;
        }
    }
}