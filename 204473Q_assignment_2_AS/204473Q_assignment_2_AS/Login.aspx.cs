using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using _204473Q_assignment_2_AS.Models;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.IO;

namespace _204473Q_assignment_2_AS
{
    public partial class Login : System.Web.UI.Page
    {
        private string hashedPassfromDB;
        private static string saltfromDB;
        static int errorCounter = 0;
        AllFunctions functions = new AllFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            string plaintextPass = HttpUtility.HtmlEncode(passTxt.Text);
            string userEmail = HttpUtility.HtmlEncode(userTxt.Text);
            registration regGet = new registration();
            saltfromDB = functions.saltCheck(userEmail);
            hashedPassfromDB = functions.hashCheck(userEmail);
            captchaObject getCaptchaResponse = functions.validateCaptcha(HttpContext.Current.Request.Form["g-recaptcha-response"]);
            if (Convert.ToBoolean(getCaptchaResponse.success)) {
                if (Convert.ToDouble(getCaptchaResponse.score) > 0.5)
                {
                    if (functions.accLockoutFunc(userEmail,false)) { 
                    if (saltfromDB != null || hashedPassfromDB != null)
                    {
                        string hashedinputpass = functions.hasher(plaintextPass, saltfromDB);
                        if (hashedPassfromDB == hashedinputpass)
                        {
                            if (ModelState.IsValid)
                            {
                                errorCounter = 0;
                                Session["LoggedIn"] = userEmail.Trim();
                                string guid = Guid.NewGuid().ToString();
                                Session["AuthToken"] = guid;
                                Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                                //audit log
                                functions.addAuditLogs(Session["LoggedIn"].ToString(), audit_Model.sessionType.Login.ToString());
                                Response.Redirect("/", false);
                            }
                        }
                        else
                        {
                            lbl_error.Text = "Wrong username or password.";
                            errorCounter++;
                            functions.lockoutCounter(userEmail, errorCounter);
                        }
                    }
                    else
                    {
                        lbl_error.Text = "Wrong username or password.";
        
                    }
                    }
                    else
                    {
                        lbl_error.Text = "Account locked out.";
                    }
                }
                else
                {
                    lbl_error.Text = "Did not pass captcha test!! <0.5";
                }
            }
            else
            {
                lbl_error.Text = "Captcha Failed, bad request!!";
            }
        }
    }
}