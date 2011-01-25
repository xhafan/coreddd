using System.Web.Mvc;

namespace EmailMaker.Controllers.Template
{
    public class TemplateController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }
    
    }
}
