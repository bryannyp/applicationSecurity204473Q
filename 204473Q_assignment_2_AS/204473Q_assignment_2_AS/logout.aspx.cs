using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _204473Q_assignment_2_AS.Models;

namespace _204473Q_assignment_2_AS
{
    public partial class logout : System.Web.UI.Page
    {
        AllFunctions functions = new AllFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null)
            {
                if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
                {
                    if (Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                    {
                        functions.addAuditLogs(Session["LoggedIn"].ToString(), audit_Model.sessionType.logout.ToString()); ;
                        Session.Clear();
                        Session.Abandon();
                        Session.RemoveAll();
                        if (Request.Cookies["AuthToken"] != null)
                        {
                            Response.Cookies["AuthToken"].Value = string.Empty;
                            Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
                        }
                        if(Request.Cookies["ASP.NET_SessionId"] != null)
                        {
                            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                        }
                        Response.Redirect("Login.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx", false);
                }

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
    }
}