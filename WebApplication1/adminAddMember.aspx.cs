using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;

namespace WebApplication1
{
    public partial class adminAddMember : System.Web.UI.Page
    {
        /*class generatePassword {
            public static string randomString;
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder(16);
            public string pass()
            {
                for (int i = 0; i < 16; i++)
                {
                    int index = random.Next(characters.Length);
                    stringBuilder.Append(characters[index]);
                    string randomString = stringBuilder.ToString();
                    
                }
                randomString = stringBuilder.ToString();
                return randomString;
            }
    };*/
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        /*protected void Button1_Click(object sender, EventArgs e)
        {
            generatePassword obj = new generatePassword();
            string password = obj.pass();
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string email = TextBox4.Text.Trim();
            string setPasswordLink = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/addMemberViaAdminPassword.aspx";
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "rishav@xorosoft.com";
            string smtpPassword = "Xorosoft";
            SmtpClient smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = "Set Password For New Registration",
                IsBodyHtml = true,
            };

            string emailBody = @"
                                <!DOCTYPE html>
                                <html lang='en'>
                                <head>
                                    <meta charset='UTF-8'>
                                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                    <title>Password Reset Request</title>
                                </head>
                                <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
                                    <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px; margin: 0 auto; background-color: #ffffff; box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.3);'>
                                        <tr>
                                            <td align='center' valign='top' style='padding: 20px 0;'>
                                                <h1 style='color: #007bff; margin-bottom: 20px;'>Password Reset Request</h1>
                                                <p>Dear User,</p>
                                                <p>You have requested to reset your password. To proceed with the password reset, please click the button below:</p>
                                                <table border='0' cellpadding='0' cellspacing='0' style='width: 100%;'>
                                                    <tr>
                                                        <td align='center' style='padding: 20px;'>
                                                            <a href='" + setPasswordLink + @"' style='display: inline-block; background-color: #007bff; color: #ffffff; text-decoration: none; padding: 15px 30px; border-radius: 5px;'>Set Password</a>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <p>If you didn't request this password , please ignore this email.</p>
                                                <p style='color: #888; font-size: 12px;'>This email was sent to you as part of a password reset request. If you have any concerns, please contact our support team.</p>
                                            </td>
                                        </tr>
                                        <tr>
                                                <td align='center' valign='top' style='padding: 20px 0;'>
                                                    <h3>Password</h3>
                                                <span>" + password+ @"</span>

                                            </td>
                                        </tr>
                                    </table>
                                </body>
                                </html>
                                ";

            mailMessage.Body = emailBody;
            mailMessage.To.Add(email);
            try
            {
                smtpClient.Send(mailMessage);
                string script = "alert('Check Your Email');";
                ClientScript.RegisterStartupScript(this.GetType(), "SucessMessage", script, true);
                setUserDetails(password);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                Session["emailSetByAdmin"] = email.ToString();
            }
        }

        public void setUserDetails(string password)
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "INSERT INTO member_master_tbl(full_name , dob , contact_no , email , state , city , pincode , full_address , member_id , password , account_status)VALUES(@full_name , @dob , @contact_no , @email , @state , @city , @pincode , @full_address , @member_id , @password , @account_status)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@full_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@member_id", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@account_status", "pending");

                cmd.ExecuteNonQuery();
                con.Close();
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {

            }
        }*/
    }
}