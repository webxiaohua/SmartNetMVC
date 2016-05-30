using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smart.NetMVC2
{
    public sealed class ControllerDescription
    {
        public Type ControllerType { get; private set; }

        public AllowRoleAttribute AllowRole { get; set; }

        public AllowUserAttribute AllowUser { get; set; }

        public ControllerDescription(Type t, AllowRoleAttribute allowRole, AllowUserAttribute allowUser)
        {
            this.ControllerType = t;
            this.AllowRole = allowRole;
            this.AllowUser = allowUser;
        }
    }
}
