import Castle.Core from Castle.Windsor
import Core.Commands
import Core.Domain
import Core.Domain.Persistence
import Core.Queries
import System.Web.Mvc

# Controllers
for type in AllTypesBased[of Controller]("EmailMaker.Controllers"):
	component type : @lifestyle=LifestyleType.Transient

component ICommandExecutor, CommandExecutor
component IQueryExecutor, QueryExecutor
component IRepository, NHRepository : @lifestyle=LifestyleType.Transient

# Command handlers
for type in AllTypes("EmailMaker.Commands") \
	.Where( { t as System.Type | t.Namespace.StartsWith("EmailMaker.Commands.Handlers") and t.GetInterfaces().Length > 0 }):
    component type.GetInterfaces()[0], type: @lifestyle=LifestyleType.Transient

# Query handlers
for type in AllTypes("EmailMaker.Queries") \
	.Where( { t as System.Type | t.Namespace.StartsWith("EmailMaker.Queries.Handlers") and t.GetInterfaces().Length > 0 }):
    component type.GetInterfaces()[0], type: @lifestyle=LifestyleType.Transient