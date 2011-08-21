import Castle.Core from Castle.Windsor
import Castle.Core.Smtp
import Core.Domain
import Core.Domain.Persistence
import Core.Queries
import EmailMaker.Domain.Services

component IQueryExecutor, QueryExecutor
component IRepository, NHRepository : @lifestyle=LifestyleType.Transient
component IEmailHtmlBuilder, EmailHtmlBuilder
component IEmailSender, DefaultSmtpSender : hostname = "localhost"

# Query handlers
for type in AllTypes("EmailMaker.Queries") \
	.Where( { t as System.Type | t.Namespace.StartsWith("EmailMaker.Queries.Handlers") and t.GetInterfaces().Length > 0 }):
    component type.GetInterfaces()[0], type: @lifestyle=LifestyleType.Transient