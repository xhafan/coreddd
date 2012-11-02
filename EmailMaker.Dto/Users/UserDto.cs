using CoreDdd.Dtos;

namespace EmailMaker.Dtos.Users
{
    public class UserDto : Dto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
