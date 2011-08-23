using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using EmailMaker.Controllers.BaseController;

namespace EmailMaker.Controllers
{
    public class HomeController : Controller 
    {
    
        public ActionResult Index()
        {
           return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
