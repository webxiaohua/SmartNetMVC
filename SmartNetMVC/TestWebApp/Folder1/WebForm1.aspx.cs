using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestWebApp.Folder1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["id"] != null)
            {
                Response.Write(Request["id"]);
            }
            /*
            if (Page.RouteData.Values.ContainsKey("id"))
            {
                Response.Write(Page.RouteData.Values["id"]);
            }
            */
            //Response.Write(parameter);
        }
    }
}