using Core.Domain;
using Core.Utilities;

namespace EmailMaker.Domain.Users
{
    //todo: persistence test is missing
    public class User : Identity<User>, IAggregateRootEntity
    {
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string EmailAddress { get; protected set; }
        public virtual string Password { get; protected set; }

        protected User(){}

        // todo missing test
        public User(string firstName,string lastName,string emailAddress,string password)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Password = password;
        }

        public virtual void ChangePassword(string oldPassword, string newPassword)
        {
            Guard.Hope(Password == oldPassword, "Old password doesnot match");
            Password = newPassword;
        }
    }
}
