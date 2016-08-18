using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smart.NetMVC2;
using Smart.NetMVC2.AOP;

namespace TestWebApp.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TestInjectControllerAttribute : Attribute, IControllerInjector
    {

        public void OnActionExecuting(ControllerDescription controllerContext)
        {
            //controllerContext.ControllerContext.Response.Write("Error");
            //controllerContext.ControllerContext.Response.End();
            controllerContext.ControllerContext.Response.Redirect("/Login/Index");
        }

        public void OnActionExecuted(ControllerDescription controllerContext)
        {
            throw new NotImplementedException();
        }
    }

    [TestInjectController]
    public class TestInjectorController : BaseController
    {
        public PageResult IndexAction(string parm1, string parm2)
        {
            return null;
        }
    }
}