﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smart.NetMVC2
{
    internal class JsonDataProvider:IActionParamProvider
    {
        public object[] GetParameters(System.Web.HttpRequest request, ActionDescription action)
        {
            throw new NotImplementedException();
        }
    }
}
