using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.MsmqIntegration;
using System.ServiceModel.Security;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebApplication1
{
    public partial class EmailSent : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString; 
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = emailTextBox.Text.Trim();
            string resetToken = Guid.NewGuid().ToString();
            setTokenKey(resetToken);
            string resetLink = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/ForgotPassword.aspx?token={resetToken}";
            string smptpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "rishav@xorosoft.com";
            string smtpPassword = "kzdo ygry ccdx dfxq";
            SmtpClient smtpClient = new SmtpClient(smptpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true,
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
                                    <a href='" + resetLink + @"' style='display: inline-block; background-color: #007bff; color: #ffffff; text-decoration: none; padding: 15px 30px; border-radius: 5px;'>Reset Password</a>
                                </td>
                            </tr>
                        </table>
                        <p>If you didn't request this password reset, please ignore this email.</p>
                        <p style='color: #888; font-size: 12px;'>This email was sent to you as part of a password reset request. If you have any concerns, please contact our support team.</p>
                    </td>
                </tr>
            </table>
        </body>
        </html>
        ";

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = "Reset Request",
                Body = emailBody,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);
            try
            {
                smtpClient.Send(mailMessage);
                string script = "alert('Check your Email'); ";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                Session["userEmail"] = email.ToString();
            }
        }

        public void setTokenKey(string resetToken)
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                if(con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "UPDATE member_master_tbl SET userKey = @userKey WHERE email = @email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@userKey", resetToken);
                cmd.Parameters.AddWithValue("@email", emailTextBox.Text.Trim());
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close();
            }
        }
    }
}