using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.TestHelper.Persistence;
using EmailMaker.DTO.Users;
using EmailMaker.Domain.Users;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.QueryTests
{
    [TestFixture]
    public class when_querying_for_user_details_by_emailaddress : BaseSimplePersistenceTest
    {

        private User _user;
        private IEnumerable<UserDTO>  _results;
        private string _firstName = "first name";
        private string _lastName = "last name";
        private string _emailAddress ="email@test.com";
        private string _password = "password";

        public override void PersistenceContext()
        {
           _user = new User(_firstName,_lastName,_emailAddress,_password);
            Save(_user);
        }

        public override void PersistenceQuery()
        {
            var query = new GetUserDetailsByEmailAddressQuery();
            _results = query.Execute<UserDTO>(new GetUserDetailsByEmailAddressMessage { EmailAddress = _emailAddress });
        }

        
        [Test]
        public void user_details_correctly_retrieved()
        {
            _results.Count().ShouldBe(1);
            var retrivedUserDetailsDTO = _results.First();
            retrivedUserDetailsDTO.FirstName.ShouldBe(_firstName);
            retrivedUserDetailsDTO.LastName.ShouldBe(_lastName);
            retrivedUserDetailsDTO.EmailAddress.ShouldBe(_emailAddress);
            retrivedUserDetailsDTO.Password.ShouldBe(_password);

        }


    }
}
