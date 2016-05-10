using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smart.NetMVC2
{
    /// <summary>
    /// 只允许post请求
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionAttribute : Attribute
    {
        public string Verb { get; set; }

        internal bool AllowExecute(string method)
        {
            if (string.IsNullOrEmpty(Verb) || Verb == "*")
            {
                return true;
            }
            else
            {
                string[] verbArray = Verb.Split(',');
                return verbArray.Contains(method, StringComparer.OrdinalIgnoreCase);
            }
        }
    }
}
