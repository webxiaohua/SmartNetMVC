using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        public string Key = "0000000";
        protected void Page_Load(object sender, EventArgs e)
        {
            //string parameter = Page.RouteData.Values["parameter"] as string;
            //Response.Write(parameter);
            Response.Redirect("AjaxTest.htm");
        }
    }
}