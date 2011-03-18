using System.Web.Mvc;
using Core.Commands;
using Core.Queries;
using EmailMaker.Commands.Messages;
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
            throw new System.NotImplementedException();
        }
    }
}