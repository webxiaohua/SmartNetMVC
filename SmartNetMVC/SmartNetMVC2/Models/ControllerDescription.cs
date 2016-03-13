using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smart.NetMVC2
{
    public sealed class ControllerDescription
    {
        public Type ControllerType { get; private set; }
        public ControllerDescription(Type t)
        {
            this.ControllerType = t;
        }
    }
}
