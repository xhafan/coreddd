using System.Web.Mvc;
using Core.Queries;
using EmailMaker.Controllers.BaseController;

namespace EmailMaker.Controllers
{
    public class HomeController : AuthenticatedController 
    {
        public HomeController(IQueryExecutor queryExecutor) : base(queryExecutor)
        {
        }

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
