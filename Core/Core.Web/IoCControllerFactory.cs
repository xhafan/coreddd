using System;
using System.Web.Mvc;
using System.Web.Routing;
using CoreIoC;

namespace Core.Web
{
    public class IoCControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController)IoC.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            IoC.Release(controller);
        }        
    }

}
