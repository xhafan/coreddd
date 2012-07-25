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
using EmailMaker.Dtos.Emails;
using EmailMaker.Queries.Messages;
using MvcContrib;

namespace EmailMaker.Controllers
{
    public class EmailController : AuthenticatedController
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly IQueryExecutor _queryExecutor;

        public EmailController(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor) : base(queryExecutor)
        {
            _queryExecutor = queryExecutor;
            _commandExecutor = commandExecutor;
        }

        public ViewResult Index()
        {
            var emailTemplates = _queryExecutor.Execute<GetAllEmailTemplateQueryMessage, EmailTemplateDetailsDto>(new GetAllEmailTemplateQueryMessage { UserId = UserId});
            var model = new TemplateIndexModel { EmailTemplate = emailTemplates };
            return View(model);
        }
        
        // todo: make it httppost
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
                                                        User.Identity.Name                                                  
                                                    },
                                ToAddresses = new[]
                                                  {
                                                      User.Identity.Name 
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

        private EmailDto _GetEmail(int id)
        {
            var message = new GetEmailQueryMessage { EmailId = id };
            var variablePartMessage = new GetEmailVariablePartsQueryMessage { EmailId = id };

            var emailDtos = _queryExecutor.Execute<GetEmailQueryMessage, EmailDto>(message);
            var variableEmailPartDtos = _queryExecutor.Execute<GetEmailVariablePartsQueryMessage, EmailPartDto>(variablePartMessage);

            var emailDto = emailDtos.Single();
            emailDto.Parts = variableEmailPartDtos;

            return emailDto;
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
            var emailPartDtos = _queryExecutor.Execute<GetEmailPartsQueryMessage, EmailPartDto>(partMessage);

            var sb = new StringBuilder();
            emailPartDtos.Each(part =>
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