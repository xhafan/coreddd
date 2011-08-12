import Castle.Core from Castle.Windsor
import EmailMaker.Service.Handlers

# Message handlers
for type in AllTypes("EmailMaker.Service") \
	.Where( { t as System.Type | t.Namespace.StartsWith("EmailMaker.Service.Handlers") and t.GetInterfaces().Length > 0 }):
    component type.GetInterfaces()[0], type: @lifestyle=LifestyleType.Transient


