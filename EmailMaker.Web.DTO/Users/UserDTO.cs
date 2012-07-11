using Core.Dtos;

namespace EmailMaker.DTO.Users
{
    public class UserDTO : Dto
    {
        public virtual int UserId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Password { get; set; }
    }
}
