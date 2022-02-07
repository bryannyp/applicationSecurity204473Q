using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _204473Q_assignment_2_AS
{
    public partial class masterpage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AllFunctions functions = new AllFunctions();
            if (Session["LoggedIn"] != null)
            {
                if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
                {
                    if (Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                    {
                        string token = Request.Cookies["AuthToken"].Value;
                        string sessionID = Request.Cookies["ASP.NET_SessionId"].Value;
                        Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMinutes(1);
                        Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMinutes(1);
                        Response.Cookies["ASP.NET_SessionId"].Value = sessionID;
                        Response.Cookies["AuthToken"].Value = token;

                        string email = Session["LoggedIn"].ToString();
                       
                        lblName.Text = $"Hello, {functions.getAllInfo(email).first_name}";
                        lblName.ForeColor = System.Drawing.Color.Green;
                        signoutAnchor.Visible = true;
                        profileAnchor.Visible = true;
                        loginAnchor.Visible = false;
                        registrationAnchor.Visible = false;
                    }
                }
                else
                {
                    lblName.Text = "Please log in!";
                    signoutAnchor.Visible = false;
                    profileAnchor.Visible = false;
                    loginAnchor.Visible=true;
                    registrationAnchor.Visible=true;
                }

            }
            else
            {
                lblName.Text = "Please log in!";
                signoutAnchor.Visible = false;
                profileAnchor.Visible = false;
                loginAnchor.Visible = true;
                registrationAnchor.Visible = true;
            }
        }
    }
}