using System;
using System.Security.Principal;

namespace EmailMaker.Web
{
    // http://www.bradygaster.com/custom-authentication-with-mvc-3.0
    public class CustomPrincipal : GenericPrincipal
    {
        public CustomPrincipal(CustomIdentity identity, string[] roles) : base(identity, roles)
        {
        }
    }
}
