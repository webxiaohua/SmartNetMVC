using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smart.NetMVC2.AOP
{
    public interface IActionInjector
    {
        void OnActionExecuting(ActionDescription actionContext);

        void OnActionExecuted(ActionDescription actionContext);
    }

    public interface IControllerInjector {
        void OnActionExecuting(ControllerDescription controllerContext);

        void OnActionExecuted(ControllerDescription controllerContext);
    }
}
