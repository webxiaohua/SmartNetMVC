﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC
{
    internal interface IActionParametersProvider
    {
        object[] GetParameters(HttpRequest request, ActionDescription action);

    }
}
