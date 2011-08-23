using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailMaker.DTO.Users
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
