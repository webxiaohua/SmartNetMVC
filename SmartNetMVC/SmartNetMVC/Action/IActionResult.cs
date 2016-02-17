using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smart.NetMVC
{
    public interface IActionResult
    {
        void Ouput(HttpContext context);
    }
}
