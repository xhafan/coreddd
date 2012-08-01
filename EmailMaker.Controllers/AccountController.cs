using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Core.Commands;
using Core.Queries;
using EmailMaker.Commands.Messages;
using EmailMaker.Controllers.ViewModels;
using EmailMaker.Dtos.Users;
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
                var userDetails  = _queryExecutor.Execute<GetUserDetailsByEmailAddressMessage, UserDto>(message).FirstOrDefault();
                
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

        public ActionResult Register()
        {
            ViewBag.PasswordLength = Membership.Provider.MinRequiredPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                var command = new CreateUserCommand { EmailAddress = model.Email, Password = model.Password};
                _commandExecutor.Execute(command);

                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = Membership.Provider.MinRequiredPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = Membership.Provider.MinRequiredPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // todo: remove this query - remember id in the same way as name
                var message = new GetUserDetailsByEmailAddressMessage { EmailAddress = User.Identity.Name };
                var user = _queryExecutor.Execute<GetUserDetailsByEmailAddressMessage, UserDto>(message).First();

                var command = new ChangePasswordForUserCommand
                                  {
                                      UserId = user.UserId,
                                      OldPassword = model.OldPassword,
                                      NewPassword = model.NewPassword
                                  };
                _commandExecutor.Execute(command);

                return RedirectToAction("ChangePasswordSuccess");
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = Membership.Provider.MinRequiredPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
