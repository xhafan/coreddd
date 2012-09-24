using System.Collections.Generic;
using System.Linq;
using CoreDdd.Tests.Helpers.Persistence;
using EmailMaker.Domain.Users;
using EmailMaker.Dtos.Users;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.Queries
{
    [TestFixture]
    public class when_querying_for_user_details_by_emailaddress : BaseSimplePersistenceTest
    {
        private User _user;
        private IEnumerable<UserDto>  _results;
        private const string FirstName = "first name";
        private const string LastName = "last name";
        private const string EmailAddress = "email@test.com";
        private const string Password = "password";

        protected override void PersistenceContext()
        {
           _user = new User(FirstName,LastName,EmailAddress,Password);
            Save(_user);
        }

        protected override void PersistenceQuery()
        {
            var query = new GetUserDetailsByEmailAddressQuery();
            _results = query.Execute<UserDto>(new GetUserDetailsByEmailAddressMessage { EmailAddress = EmailAddress });
        }

        
        [Test]
        public void user_details_correctly_retrieved()
        {
            var retrivedUserDetailsDto = _results.Single();
            retrivedUserDetailsDto.FirstName.ShouldBe(FirstName);
            retrivedUserDetailsDto.LastName.ShouldBe(LastName);
            retrivedUserDetailsDto.EmailAddress.ShouldBe(EmailAddress);
            retrivedUserDetailsDto.Password.ShouldBe(Password);

        }
    }
}
