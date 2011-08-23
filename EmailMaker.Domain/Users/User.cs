using Core.Domain;

namespace EmailMaker.Domain.Users
{
    public class User : Identity<User>, IAggregateRootEntity
    {
        public virtual string FirstName { get; private set; }
        public virtual string LastName { get; private set; }
        public virtual string EmailAddress { get; private set; }
        public virtual string Password { get; private set; }

        protected User(){}

        public User(string firstName,string lastName,string emailAddress,string password)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Password = password;
        }
    }
}
