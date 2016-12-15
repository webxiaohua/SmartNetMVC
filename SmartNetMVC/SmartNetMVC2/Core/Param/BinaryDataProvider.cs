using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Smart.NetMVC2
{
    public class BinaryDataProvider : IActionParamProvider
    {
        public object[] GetParameters(System.Web.HttpRequest request, ActionDescription action)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if (action == null)
                throw new ArgumentNullException("action");
            object[] parameters = new object[action.Parameters.Length];
            byte[] input = request.BinaryRead(request.TotalBytes);
            for (int i = 0; i < action.Parameters.Length; i++)
            {
                ParameterInfo p = action.Parameters[i];
                if (p.IsOut)
                    continue;
                if (p.ParameterType == typeof(byte[]))
                {
                    parameters[i] = input;
                }
            }
            return parameters;
        }
    }
}
