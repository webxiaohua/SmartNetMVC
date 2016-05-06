using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC2
{
    internal interface IActionParamProvider
    {
        object[] GetParameters(HttpRequest request, ActionDescription action);
    }
}
