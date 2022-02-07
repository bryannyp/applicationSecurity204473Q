using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _204473Q_assignment_2_AS.Models;
using System.Text;

namespace _204473Q_assignment_2_AS
{
    public partial class profile : System.Web.UI.Page
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
                        dataModel data = functions.getAllInfo(Session["LoggedIn"].ToString());
                        Label1.Text += data.first_name;
                        Label2.Text += data.last_name;
                        Label3.Text += functions.decrypter(data.credit_card_info, Convert.FromBase64String(data.IV),Convert.FromBase64String(data.cryptokey));
                        Label4.Text += data.email;
                        Label5.Text += data.date_of_birth;
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