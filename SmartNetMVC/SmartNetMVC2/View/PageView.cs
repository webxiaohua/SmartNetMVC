using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smart.NetMVC2
{
    public class PageView<T> : BasePage
    {
        public T Model { get; set; }
        public override void SetModel(object model)
        {
            try
            {
                this.Model = (T)model;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("参数model与目标类型不匹配", ex);
            }
        }
    }
}
