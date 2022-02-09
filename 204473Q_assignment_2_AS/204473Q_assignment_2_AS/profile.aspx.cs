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
                        lbl_firstName.Text = data.first_name;
                        lblLastName.Text = data.last_name;
                        lblCreditCard.Text = functions.decrypter(data.credit_card_info, Convert.FromBase64String(data.IV),Convert.FromBase64String(data.cryptokey));
                        lblEmail.Text = data.email;
                        lblDateofBirth.Text = data.date_of_birth;
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

        protected void passChangeBtn_Click(object sender, EventArgs e)
        {
            if (functions.hasher(oldPassTxt.Text, functions.saltCheck(Session["LoggedIn"].ToString())) == functions.getAllInfo(Session["LoggedIn"].ToString()).password)
            {
                if (newPassTxt.Text == matchPasstxt.Text)
                {
                    if(functions.updatePass(Session["LoggedIn"].ToString(), newPassTxt.Text))
                    {
                        ModelState.AddModelError(string.Empty, "password changed!");
                        ErrorSummary.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Something wrong D:");
                    }
                   
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "password does not match!");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty,"Wrong password!");
            }
         
        }
    }
}