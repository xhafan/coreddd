using System.Linq;
using System.Web.Mvc;
using Core.Commands;
using Core.Queries;
using EmailMaker.Commands.Messages;
using EmailMaker.Controllers.ViewModels;
using EmailMaker.DTO.Emails;
using EmailMaker.Queries.Messages;
using MvcContrib;

namespace EmailMaker.Controllers
{
    public class EmailController : Controller
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;

        public EmailController(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
        }

        // todo: make httppost
        public ActionResult Create(int id)
        {
            var createdEmailId = default(int);
            var command = new CreateEmailCommand {EmailTemplateId = id};
            _commandExecutor.CommandExecuted += (sender, args) => createdEmailId = (int)args.Args;
            _commandExecutor.Execute(command);

            return this.RedirectToAction(a => a.Edit(createdEmailId));
        }

        public ActionResult Edit(int id)
        {
            var email = _GetEmail(id);
            var model = new EmailEditModel { Email = email };
            return View(model);
        }

        private EmailDTO _GetEmail(int id)
        {
            var message = new GetEmailQueryMessage { EmailId = id };
            var partMessage = new GetEmailPartsQueryMessage { EmailId = id };

            var emailDTOs = _queryExecutor.Execute<GetEmailQueryMessage, EmailDTO>(message);
            var emailPartDTOs = _queryExecutor.Execute<GetEmailPartsQueryMessage, EmailPartDTO>(partMessage);

            var emailDTO = emailDTOs.Single();
            emailDTO.Parts = emailPartDTOs;

            return emailDTO;
        }

        [HttpPost]
        public void UpdateVariables(UpdateEmailVariablesCommand command)
        {
            throw new System.NotImplementedException();
            _commandExecutor.Execute(command);
        }

        [HttpPost]
        public ActionResult GetEmail(int id)
        {
            return Json(_GetEmail(id));
        }
    }
}