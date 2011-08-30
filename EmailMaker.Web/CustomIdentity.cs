using System.Security.Principal;

namespace EmailMaker.Web
{
    // http://www.bradygaster.com/custom-authentication-with-mvc-3.0
    public class CustomIdentity : GenericIdentity
    {
        public int UserId { get; private set; }

        public CustomIdentity(string name) : base(name) {}
        public CustomIdentity(string name, string type) : base(name, type) { }
        
        public CustomIdentity(int userId, string name, string type) : base(name, type)
        {
            UserId = userId;
        }
    }
}