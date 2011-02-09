using System.Collections.Generic;
using System.Web.Mvc;
using EmailMaker.Controllers.ViewModels;
using EmailMaker.Web.DTO.EmailTemplate;

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

        public ActionResult Edit(int id)
        {
            var emailTemplate = new EmailTemplateDTO
                                    {
                                        EmailTemplateId = id,
                                        Parts = new[]
                                                    {
                                                        new EmailPartDTO
                                                            {
                                                                Html = "html1"
                                                            },
                                                        new EmailPartDTO
                                                            {
                                                                VariableValue = "value1"
                                                            },
                                                        new EmailPartDTO
                                                            {
                                                                Html = "html2"
                                                            },
                                                    }
                                    };
            var model = new EmailTemplateEditModel
                            {
                                EmailTemplate = emailTemplate
                            };
            return View(model);
        }
    }
}
