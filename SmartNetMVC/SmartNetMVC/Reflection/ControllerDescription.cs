using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smart.NetMVC
{
    internal sealed class ControllerDescription : BaseDescription
    {
        public Type ControllerType { get; private set; }

        public ControllerDescription(Type t)
            : base(t)
        {
            this.ControllerType = t;
        }
    }
}
