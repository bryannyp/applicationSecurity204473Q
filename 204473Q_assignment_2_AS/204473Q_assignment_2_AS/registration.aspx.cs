using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using _204473Q_assignment_2_AS.Models;
namespace _204473Q_assignment_2_AS
{
    public partial class registration : System.Web.UI.Page
    {
        static string finalHash;
        static string salt;
      
        AllFunctions functions = new AllFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_submit_Click1(object sender, EventArgs e)
        {
            captchaObject getCaptchaResponse = functions.validateCaptcha(HttpContext.Current.Request.Form["g-recaptcha-response"]);
            if (Convert.ToBoolean(getCaptchaResponse.success))
            {
                if (Convert.ToDouble(getCaptchaResponse.score) > 0.5)
                {
                    if (ModelState.IsValid)
                    {
                        string password = password_txt.Text.ToString().Trim();
                        finalHash = functions.hasher(password);
                        if (!functions.emailCheck(email_txt.Text))
                        {
                            if (accountCreate())
                            {
                                Response.Redirect("/", false);
                            }
                            else
                            {
                            }
                        }
                        else
                        {

                            ModelState.AddModelError(string.Empty, "Account already exists!!");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Did not pass captcha test!! <0.5");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Captcha Failed, bad request!!");
            }
        }
       
        protected bool accountCreate()
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            bool checkerBool = true;
            try{
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO AccountsDB(First_Name,Last_Name,Credit_Card_info,Email_address,password,Date_of_birth,photo,IV,cryptokey,salt) VALUES (@first,@last,@creditCard,@email,@password,@dateBirth,@photo,@IV,@key,@salt)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            SqlParameter[] param = new SqlParameter[10];
                            param[0] = new SqlParameter("@first", HttpUtility.HtmlEncode( firstname_txt.Text));
                            param[1] = new SqlParameter("@last", HttpUtility.HtmlEncode(lastname_txt.Text));
                            param[2] = new SqlParameter("@creditCard", HttpUtility.HtmlEncode(Convert.ToBase64String(functions.crypter(creditcard_txt.Text))));
                            param[3] = new SqlParameter("@email",  HttpUtility.HtmlEncode(email_txt.Text));
                            param[4] = new SqlParameter("@password", HttpUtility.HtmlEncode(finalHash.ToString()));
                            param[5] = new SqlParameter("@dateBirth", HttpUtility.HtmlEncode(dateofBirth_cal.Value));
                            param[6] = new SqlParameter("@photo",photoUpload_fu.FileBytes);
                            param[7] = new SqlParameter("@IV",Convert.ToBase64String(functions.IV));
                            param[8] = new SqlParameter("@key", Convert.ToBase64String(functions.Key));
                            param[9] = new SqlParameter("@salt", functions.salt);

                            cmd.Parameters.Add(param[0]);
                            cmd.Parameters.Add(param[1]);
                            cmd.Parameters.Add(param[2]);
                            cmd.Parameters.Add(param[3]);
                            cmd.Parameters.Add(param[4]);
                            cmd.Parameters.Add(param[5]);
                            cmd.Parameters.Add(param[6]);
                            cmd.Parameters.Add(param[7]);
                            cmd.Parameters.Add(param[8]);
                            cmd.Parameters.Add(param[9]);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            functions.addAuditLogs(email_txt.Text, audit_Model.sessionType.register.ToString());
                        }
                    }
                }
                return checkerBool;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                checkerBool = false;
                return checkerBool;
            }
        }
    }
}