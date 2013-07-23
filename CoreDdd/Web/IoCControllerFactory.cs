using System;
using System.Web.Mvc;
using System.Web.Routing;
using CoreIoC;

namespace CoreDdd.Web
{
    public class IoCControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, null);
            }
            return (IController)IoC.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            IoC.Release(controller);
        }        
    }

}
