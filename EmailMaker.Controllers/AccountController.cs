using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Core.Commands;
using Core.Queries;
using EmailMaker.Controllers.ViewModels;
using EmailMaker.DTO.Users;
using EmailMaker.Domain.Users;
using EmailMaker.Queries.Messages;

namespace EmailMaker.Controllers
{
    public class AccountController : Controller
    {

        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;

        public AccountController(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
        }


        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var message = new GetUserDetailsByEmailAddressMessage { EmailAddress = model.EmailAddress };
                var userDetails  = _queryExecutor.Execute<GetUserDetailsByEmailAddressMessage, UserDTO>(message).FirstOrDefault();
                
                if(userDetails != null)
                {
                    if(userDetails.Password.Equals(model.Password.Trim()))
                    {
                        FormsAuthentication.SetAuthCookie(model.EmailAddress, model.RememberMe);
                       
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                   
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");

                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
