using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smart.NetMVC2.AOP;
using System.Web;

namespace Smart.NetMVC2
{
    public sealed class ControllerDescription
    {
        public Type ControllerType { get; private set; }

        public AllowRoleAttribute AllowRole { get; set; }

        public AllowUserAttribute AllowUser { get; set; }

        public HttpContext ControllerContext { get; set; }
        /// <summary>
        /// 注入器
        /// </summary>
        public IControllerInjector Injector { get; set; }

        public ControllerDescription(Type t, AllowRoleAttribute allowRole, AllowUserAttribute allowUser)
        {
            this.ControllerType = t;
            this.AllowRole = allowRole;
            this.AllowUser = allowUser;
        }

        public ControllerDescription(Type t, AllowRoleAttribute allowRole, AllowUserAttribute allowUser, IControllerInjector injector)
        {
            this.ControllerType = t;
            this.AllowRole = allowRole;
            this.AllowUser = allowUser;
            this.Injector = injector;
        }
    }
}
