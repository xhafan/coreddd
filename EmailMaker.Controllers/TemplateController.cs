using System.Linq;
using System.Text;
using System.Web.Mvc;
using Core.Commands;
using Core.Queries;
using Core.Utilities.Extensions;
using EmailMaker.Commands.Messages;
using EmailMaker.Controllers.BaseController;
using EmailMaker.Controllers.ViewModels;
using EmailMaker.Core;
using EmailMaker.Dtos;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Messages;
using MvcContrib;

namespace EmailMaker.Controllers
{
    public class TemplateController : AuthenticatedController
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;

        public TemplateController(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor) : base(queryExecutor)
        {
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
        }

        public ActionResult Index()
        {
            var emailTemplates = _queryExecutor.Execute<GetAllEmailTemplateQueryMessage, EmailTemplateDetailsDto>(new GetAllEmailTemplateQueryMessage{ UserId = UserId });           
            var model = new TemplateIndexModel { EmailTemplate = emailTemplates };
            return View(model);
        }

        // todo: make httppost
        public ActionResult Create()
        {            
            var createdEmailTemplateId = default(int);
            var command = new CreateEmailTemplateCommand { UserId = UserId };
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

        private EmailTemplateDto _GetEmailTemplate(int id)
        {
            var templateMessage = new GetEmailTemplateQueryMessage {EmailTemplateId = id};
            var templatePartMessage = new GetEmailTemplatePartsQueryMessage { EmailTemplateId = id };

            var emailTemplateDTOs = _queryExecutor.Execute<GetEmailTemplateQueryMessage, EmailTemplateDto>(templateMessage);
            var emailTemplatePartDTOs = _queryExecutor.Execute<GetEmailTemplatePartsQueryMessage, EmailTemplatePartDto>(templatePartMessage);
            
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

        public string GetHtml(int id)
        {
            var partMessage = new GetEmailTemplatePartsQueryMessage { EmailTemplateId = id };
            var emailTemplatePartDTOs = _queryExecutor.Execute<GetEmailTemplatePartsQueryMessage, EmailTemplatePartDto>(partMessage);

            var sb = new StringBuilder();
            emailTemplatePartDTOs.Each(part =>
            {
                if (part.PartType == PartType.Html)
                {
                    sb.Append(part.Html);
                }
                else if (part.PartType == PartType.Variable)
                {
                    sb.Append(part.VariableValue);
                }
                else
                {
                    throw new EmailMakerException("Unknown part type:" + part.PartType);
                }

            });
            return sb.ToString();
        }
    
    }
}
