using System.Linq;
using System.Web.Mvc;
using Core.Commands;
using Core.Queries;
using EmailMaker.Commands.Messages;
using EmailMaker.Controllers.ViewModels;
using EmailMaker.DTO.EmailTemplate;
using EmailMaker.Queries.Messages;

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
            var templateMessage = new GetEmailTemplateQueryMessage {EmailTemplateId = id};
            var templatePartMessage = new GetEmailTemplatePartsQueryMessage { EmailTemplateId = id };
            var emailTemplateDTOs = _queryExecutor.Execute<GetEmailTemplateQueryMessage, EmailTemplateDTO>(templateMessage);
            var emailTemplatePartDTOs = _queryExecutor.Execute<GetEmailTemplatePartsQueryMessage, EmailTemplatePartDTO>(templatePartMessage);
            var emailTemplateDTO = emailTemplateDTOs.Single();
            emailTemplateDTO.Parts = emailTemplatePartDTOs;

            return emailTemplateDTO;
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
        public void DeleteVariable(DeleteVariableCommand command)
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
