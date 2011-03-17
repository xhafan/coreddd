using System.Linq;
using System.Web.Mvc;
using Core.Commands;
using Core.Queries;
using EmailMaker.Commands.Messages;
using EmailMaker.Controllers.ViewModels;
using EmailMaker.DTO.EmailTemplate;
using EmailMaker.Queries.Messages;
using MvcContrib;

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
            var emailTemplate = new EmailTemplateDTO{ Parts = new[] {new EmailTemplatePartDTO { EmailTemplatePartType = EmailTemplatePartType.Html }}};
            var model = new EmailTemplateEditModel {EmailTemplate = emailTemplate};
            return View(model);
        }

        public ActionResult Create()
        {
            var createdEmailTemplateId = default(int);
            var command = new CreateEmailTemplateCommand();
            _commandExecutor.CommandExecuted += (sender, args) => createdEmailTemplateId = (int) args.Args;
            _commandExecutor.Execute(command);

            return this.RedirectToAction(a => a.Edit(createdEmailTemplateId));
        }

        public ActionResult Edit(int id)
        {
            var emailTemplate = _GetEmailTemplate(id);
            var model = new EmailTemplateEditModel {EmailTemplate = emailTemplate};
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
        public void Save(SaveEmailTemplateCommand command)
        {
            _commandExecutor.Execute(command);
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
