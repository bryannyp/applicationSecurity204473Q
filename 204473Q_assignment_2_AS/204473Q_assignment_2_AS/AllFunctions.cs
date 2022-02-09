using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using _204473Q_assignment_2_AS.Models;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Net.Http;

namespace _204473Q_assignment_2_AS
{
    public class AllFunctions
    {
        public byte[] Key;
        public byte[] IV;
        public string salt;
        string constr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        //Update password function

        public bool updatePass(string input,string newpass)
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("update accountsDB set password=@password where email_address=@userEmail"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        string hashedpass= hasher(newpass);
                        updateSalt(input);
                        cmd.Parameters.AddWithValue("@password", hashedpass);
                        cmd.Parameters.AddWithValue("@userEmail", input);
                        cmd.Connection = con;
                        con.Open();
                        try
                        {
                            cmd.ExecuteScalar();
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        public bool updateSalt(string input)
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("update accountsDB set salt=@salt where email_address=@userEmail"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        
                        cmd.Parameters.AddWithValue("@salt", salt);
                        cmd.Parameters.AddWithValue("@userEmail", input);
                        cmd.Connection = con;
                        con.Open();
                        try
                        {
                            cmd.ExecuteScalar();
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
        }

        //lockout functions
        public void lockoutCounter(string email,int counter)
        {
            if(counter >= 3)
            {
                accLockoutFunc(email, true);
            }
        }

        public bool accLockoutFunc(string email, bool lockoutBool)
        {
            bool result = false;
            lockoutObj model = null;
            //runs adder funcvv
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (lockoutBool == true)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO lockout_table(email,accountLockoutExpire) VALUES(@email,@accountExpireDateTime)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@accountExpireDateTime", DateTime.Now.AddMinutes(5));
                            con.Open();
                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            finally
                            {
                                con.Close();
                            }
                        }
                    }
                }
                else
                {
                    //runs checker funvv
                    using (SqlCommand cmd = new SqlCommand("select * from lockout_table where email= @email"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            con.Open();
                            cmd.Parameters.AddWithValue("@email", email);
                            SqlDataReader reader = cmd.ExecuteReader();
                            try
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        model = new lockoutObj(reader.GetString(0),reader.GetDateTime(1));
                                    }
                                    result = dateTimeCompareFunc(model);
                                }
                                else
                                {
                                    result = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                result = false;
                                Console.WriteLine(ex.ToString());
                            }
                            finally
                            {
                                con.Close();
                            }
                        }
                    }
                }
            }
            return result;
        }

        public bool dateTimeCompareFunc(lockoutObj model)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM lockout_table WHERE email=@email"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        try
                        {
                            if (DateTime.Now >= model.accountExpireDate)
                            {
                                cmd.Parameters.AddWithValue("@email", model.email);
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            result = false;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            return result;
        }
        //captcha
        public captchaObject validateCaptcha(string response)
        {
            captchaObject captchaReturn = null;
            var secret = "SECRET KEY";
            var req =
               (HttpWebRequest)
                   WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=" + secret + "&response=" + HttpContext.Current.Request.Form["g-recaptcha-response"]);
            req.Method = "GET";
            try
            {
                using (WebResponse webResp = req.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(webResp.GetResponseStream()))
                    {
                        string getJson = reader.ReadToEnd();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        captchaObject captchaObj = js.Deserialize<captchaObject>(getJson);
                        captchaReturn = captchaObj;
                    }
                }
                return captchaReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //audit logs commands
        public void addAuditLogs(string userEmail, string sessionType)
        {
            DateTime currentDateTime = DateTime.Now;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO audit_logs(email,type,date_and_time) VALUES(@email,@type,@date_and_time)"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@email", userEmail);
                        cmd.Parameters.AddWithValue("@type", sessionType);
                        cmd.Parameters.AddWithValue("@date_and_time", currentDateTime);
                        con.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        //checkers

        public dataModel getAllInfo(string userEmail)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from accountsDB where email_address=@userEmail"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        dataModel data = null;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@userEmail", userEmail);
                        string hello = cmd.CommandText;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        try
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data = new dataModel(reader["first_name"].ToString(), reader["last_name"].ToString(), reader["credit_card_info"].ToString(), reader["email_address"].ToString(), reader["password"].ToString(), Convert.ToDateTime(reader["date_of_birth"]).ToString("dd/MM/yyyy"), Encoding.ASCII.GetBytes(reader["photo"].ToString()), reader["IV"].ToString(), reader["cryptokey"].ToString(), reader["salt"].ToString());
                                }
                                return data;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }





            }
        }
        public string saltCheck(string input)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select salt from accountsDB where email_address=@userEmail"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@userEmail", input);
                        cmd.Connection = con;
                        con.Open();
                        try
                        {
                            if (cmd.ExecuteScalar() != null)
                            {
                                string saltfromDB = cmd.ExecuteScalar().ToString();
                                return saltfromDB;
                            }
                            else
                            {
                                return null;
                            }

                        }
                        catch (Exception)
                        {
                            return null;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        public bool emailCheck(string input)
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select email_address from accountsDB where email_address=@userEmail"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@userEmail", input);
                        cmd.Connection = con;
                        con.Open();
                        try
                        {
                            if (cmd.ExecuteScalar() != null)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        }
                        catch (Exception)
                        {
                            return false;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        public string passCheck(string input)
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select password from accountsDB where email_address=@userEmail"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@userEmail", input);
                        cmd.Connection = con;
                        con.Open();
                        try
                        {
                            if (cmd.ExecuteScalar() != null)
                            {
                                string pass = cmd.ExecuteScalar().ToString();
                                return pass;
                            }
                            else
                            {
                                return null;
                            }

                        }
                        catch (Exception)
                        {
                            return null;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        public string hashCheck(string input)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select password from accountsDB where email_address=@userEmail"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@userEmail", input);
                        cmd.Connection = con;
                        con.Open();
                        try
                        {
                            if (cmd.ExecuteScalar() != null)
                            {
                                string hashedPassfromDB = cmd.ExecuteScalar().ToString();
                                return hashedPassfromDB;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
        }
        //crypter/decrypters and hashers
        public string hasher(string password, string saltinp = null)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[16];
            rng.GetBytes(saltByte);
            if (saltinp == null)
            {
                salt = Convert.ToBase64String(saltByte);
            }
            else
            {
                salt = saltinp;
            }
            SHA512Managed hashing = new SHA512Managed();
            string passwithSalt = password + salt;
            byte[] hashwithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(passwithSalt));
            string finalHash = Convert.ToBase64String(hashwithSalt);
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;
            return finalHash;
        }
        public byte[] crypter(string data)
        {

            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0,
               plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return cipherText;
        }
        public string decrypter(string data, byte[] IVinp, byte[] keyinp)
        {
            string plaintext = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IVinp;
                cipher.Key = keyinp;
                ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] passwordByte = Convert.FromBase64String(data);
                using (MemoryStream memDecrypt = new MemoryStream(passwordByte))
                {
                    using (CryptoStream cryptoDecrypt = new CryptoStream(memDecrypt, decryptTransform, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamDecrypt = new StreamReader(cryptoDecrypt))
                        {
                            plaintext = streamDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return plaintext;
        }
    }
}