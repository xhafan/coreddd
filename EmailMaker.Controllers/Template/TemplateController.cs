using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Commands;
using Core.Queries;
using Core.Utilities.Extensions;
using EmailMaker.Commands.Messages;
using EmailMaker.Controllers.ViewModels;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Queries.Messages;
using EmailMaker.Web.DTO.EmailTemplate;

namespace EmailMaker.Controllers.Template
{
    public class TemplateController : Controller
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;

        public TemplateController(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
        }

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
            var message = new GetEmailTemplateQueryMessage {TemplateId = id};
            var emailTemplateDTO = _queryExecutor.Execute<GetEmailTemplateQueryMessage, EmailTemplate, EmailTemplateDTO>(message,
                emailTemplate =>
                    {
                        var parts = new List<EmailTemplatePartDTO>();
                        var templateDTO = new EmailTemplateDTO { EmailTemplateId = emailTemplate.Id, Parts = parts};
                        emailTemplate.Parts.Each(part =>
                                                     {
                                                         if (part is HtmlEmailTemplatePart)
                                                         {
                                                             var htmlPart = (HtmlEmailTemplatePart)part;
                                                             parts.Add(new EmailTemplatePartDTO { PartId = part.Id, Html = htmlPart.Html });
                                                         }
                                                         if (part is VariableEmailTemplatePart)
                                                         {
                                                             var variablePart = (VariableEmailTemplatePart)part;
                                                             parts.Add(new EmailTemplatePartDTO { PartId = part.Id, VariableValue = variablePart.Value });
                                                         }
                                                     });
                        return templateDTO;
                    });

            return emailTemplateDTO.Single();
        }

        [HttpPost]
        public void Save(EmailTemplateDTO emailTemplate)
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public void CreateVariable(CreateVariableCommand command)
        {
            _commandExecutor.Execute(command);
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
