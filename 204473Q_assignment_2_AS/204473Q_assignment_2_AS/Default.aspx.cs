using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _204473Q_assignment_2_AS
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null)
            {
                if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
                {
                    if (Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                    {
                        lbl_username.Text = "Congratulations !, you are logged in.";
                        lbl_username.ForeColor = System.Drawing.Color.Green;
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