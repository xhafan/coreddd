using System.Linq;
using System.Text;
using System.Web.Mvc;
using Core.Commands;
using Core.Queries;
using Core.Utilities.Extensions;
using EmailMaker.Commands.Messages;
using EmailMaker.Controllers.ViewModels;
using EmailMaker.DTO;
using EmailMaker.DTO.Emails;
using EmailMaker.Queries.Messages;
using EmailMaker.Utilities;
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

            return this.RedirectToAction(a => a.EditVariables(createdEmailId));
        }

        public ActionResult EditVariables(int id)
        {
            var email = _GetEmail(id);
            var model = new EmailEditVariablesModel { Email = email };
            return View(model);
        }

        public ActionResult EditRecipients(int id)
        {
            var model = new EmailEditRecipientsModel
                            {
                                EmailId = id,
                                FromAddresses = new[]
                                                    {
                                                        "xhafan@gmail.com"                                                     
                                                    },
                                ToAddresses = new[]
                                                  {
                                                      "Tomas Marny <haslik@centrum.cz>",
                                                      "Tomas Marny <xhafan@gmail.com>"
                                                  },
                                Subject = "subject"
                            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EnqueueEmailToBeSent(EnqueueEmailToBeSentCommand command)
        {
            _commandExecutor.Execute(command);
            return new EmptyResult();
        }

        private EmailDTO _GetEmail(int id)
        {
            var message = new GetEmailQueryMessage { EmailId = id };
            var variablePartMessage = new GetEmailVariablePartsQueryMessage { EmailId = id };

            var emailDTOs = _queryExecutor.Execute<GetEmailQueryMessage, EmailDTO>(message);
            var variableEmailPartDTOs = _queryExecutor.Execute<GetEmailVariablePartsQueryMessage, EmailPartDTO>(variablePartMessage);

            var emailDTO = emailDTOs.Single();
            emailDTO.Parts = variableEmailPartDTOs;

            return emailDTO;
        }

        [HttpPost]
        public void UpdateVariables(UpdateEmailVariablesCommand command)
        {
            _commandExecutor.Execute(command);
        }

        [HttpPost]
        public ActionResult GetEmail(int id)
        {
            return Json(_GetEmail(id));
        }
 
        public string GetHtml(int id)
        {
            var partMessage = new GetEmailPartsQueryMessage { EmailId = id };
            var emailPartDTOs = _queryExecutor.Execute<GetEmailPartsQueryMessage, EmailPartDTO>(partMessage);

            var sb = new StringBuilder();
            emailPartDTOs.Each(part =>
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