using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Smart.NetMVC2
{
    /// <summary> 
    /// 一个基于“System.Web.UI.Page”的类  声明这个类可以被 ViewPageControlBuilder 转换
    /// </summary>
    [FileLevelControlBuilder(typeof(ViewPageControlBuilder))]
    public class BasePage : System.Web.UI.Page
    {
        public virtual void SetModel(object model) { }
    }
}
