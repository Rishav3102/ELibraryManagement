using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1
{

    public partial class userlogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                string query = "SELECT * FROM member_master_tbl where member_id = @member_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        string encryptedPass = dr.GetValue(9).ToString().Trim();
                        string decryptedPass = EncryptionDecryption.Decrypt(encryptedPass).Trim();
                        if (decryptedPass == TextBox2.Text.Trim())
                        {
                            
                            Session["username"] = dr.GetValue(8).ToString();
                            Session["fullname"] = dr.GetValue(0).ToString();
                            Session["role"] = "user";
                            Session["status"] = dr.GetValue(10).ToString(); 
                            string script = "alert('Login Successful'); window.location.href = 'homepage.aspx';";
                            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid Credentials')</script>");

                        }

                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid Credentials')</script>");

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {

            }
        }
    }
}