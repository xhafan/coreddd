import Castle.Core from Castle.Windsor
import Core.Queries

component IQueryExecutor, QueryExecutor

# Query handlers
for type in AllTypes("EmailMaker.Queries") \
	.Where( { t as System.Type | t.Namespace.StartsWith("EmailMaker.Queries.Handlers") and t.GetInterfaces().Length > 0 }):
    component type.GetInterfaces()[0], type: @lifestyle=LifestyleType.Transient