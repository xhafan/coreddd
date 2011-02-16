using System.Linq;
using System.Web.Mvc;
using EmailMaker.Commands.Messages;
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
            var emailTemplate = _GetEmailTemplate(id);
            var model = new EmailTemplateEditModel
                            {
                                EmailTemplate = emailTemplate
                            };
            return View(model);
        }

        private EmailTemplateDTO _GetEmailTemplate(int id)
        {
            return new EmailTemplateDTO
                       {
                           EmailTemplateId = id,
                           Parts = new[]
                                       {
                                           new EmailTemplatePartDTO
                                               {
                                                   PartId = 1,
                                                   Html = "html1"
                                               },
                                           new EmailTemplatePartDTO
                                               {
                                                   PartId = 2,
                                                   VariableValue = "value1"
                                               },
                                           new EmailTemplatePartDTO
                                               {
                                                   PartId = 3,
                                                   Html = "html2"
                                               },
                                       }.ToList()
                       };
        }

        [HttpPost]
        public void Save(EmailTemplateDTO emailTemplate)
        {
            
        }

        [HttpPost]
        public void CreateVariable(CreateVariableCommand command)
        {

        }

        [HttpPost]
        public ActionResult GetEmailTemplate(int id)
        {
            return Json(_GetEmailTemplate(id));
        }

        public ActionResult Edit2()
        {
            return View();
        }
    }
}
