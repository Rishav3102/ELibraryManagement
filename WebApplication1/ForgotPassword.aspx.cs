using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string currEmail = Session["userEmail"].ToString();
                newEmail.Text = currEmail;
                SqlConnection con = new SqlConnection(strcon);
                try
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "SELECT userKey FROM member_master_tbl WHERE email = @email";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@email", currEmail);
                    string userKey = cmd.ExecuteScalar().ToString();
                    string resetTokenFromURL = Request.QueryString["token"];
                    if (userKey != resetTokenFromURL) Response.Redirect("usersignup.aspx");
                    
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");
                }
                finally
                {
                    con.Close();
                    Session["role"] = "user";
                }
            }
            catch(Exception ex) { 
                Response.Write("<script>alert('First Enter a mail')</script>");
            }
            
        }
        //RESET PASSWORD
        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                string currEmail = Session["userEmail"].ToString();
                string password = TextBox1.Text.Trim();
                string confrimPassword = TextBox2.Text.Trim();
                if(password == confrimPassword)
                {
                    password = EncryptionDecryption.Encrypt(password);
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "UPDATE member_master_tbl SET password = @password WHERE email = @email";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@password" , password);
                    cmd.Parameters.AddWithValue("@email", currEmail);
                    cmd.ExecuteNonQuery();
                    string script = "alert('Password Updated Successful'); window.location.href = 'userlogin.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                }
                else
                {
                    Response.Write("<script>alert('Password does not match')</script>");
                }
                
            }
            catch(Exception ex )
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close() ;
            }
        }
    }
}