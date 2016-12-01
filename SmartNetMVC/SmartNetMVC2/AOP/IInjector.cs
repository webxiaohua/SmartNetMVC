using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smart.NetMVC2.AOP
{
    public abstract class IActionInjector : Attribute
    {
        public abstract void OnActionExecuting(ActionDescription actionContext);

        public abstract void OnActionExecuted(ActionDescription actionContext);
    }

    public abstract class IControllerInjector : Attribute
    {
        public abstract void OnControllerExecuting(ControllerDescription controllerContext);

        public abstract void OnControllerExecuted(ControllerDescription controllerContext);
    }
}
