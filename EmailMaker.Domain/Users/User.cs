using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;

namespace EmailMaker.Domain.Users
{
    public class User : Identity<User>, IAggregateRootEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Password { get; set; }

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
