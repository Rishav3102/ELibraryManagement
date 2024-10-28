using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WebApplication1
{
    public partial class userprofile : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Session Expired Login Again')</script>");
                Response.Redirect("userlogin.aspx");
            }

            else
            {
                getUserBookDetails();
                if (!Page.IsPostBack)
                {
                    getUserPersonalDetails();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Session Expired Login Again')</script>");
                Response.Redirect("userlogin.aspx");
            }

            else
            {
                updateUserPersonalDetails();
            }
        }

        //USER DEFINED FUNCTIONS


        public void updateUserPersonalDetails()
        {
            try
            {
                string password = EncryptionDecryption.Encrypt(TextBox10.Text.Trim());
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "update member_master_tbl set full_name = @full_name, dob = @dob, contact_no = @contact_no, email = @email, state = @state, city = @city, pincode = @pincode, full_address = @full_address, password = @password WHERE member_id = '" + Session["username"].ToString().Trim() + "'";

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
                    cmd.Parameters.AddWithValue("@loginFirstTime", 2);


                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Details Updates Successfully')</script>");
                    getUserPersonalDetails();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");
                }
            }
            catch(Exception ex) { 
            
            }
            finally
            {

            }
        }
        public void getUserBookDetails()
        {
            try
            {
                using(SqlConnection con = new SqlConnection(strcon))
                {
                    if(con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "SELECT * FROM book_issue_tbl WHERE member_id = '" + Session["username"].ToString() +"'";
                    using(SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        GridView1.DataSource = dt;
                        GridView1.DataBind();   
                    }

                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
               
            }
        }

        public void getUserPersonalDetails()
        {
            try
            {
                using(SqlConnection con = new SqlConnection(strcon))
                {
                    if(con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "SELECT * FROM member_master_tbl WHERE member_id = '" + Session["username"].ToString() + "'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        TextBox3.Text = dt.Rows[0]["full_name"].ToString();
                        TextBox2.Text = dt.Rows[0]["dob"].ToString();
                        TextBox1.Text = dt.Rows[0]["contact_no"].ToString();
                        TextBox4.Text = dt.Rows[0]["email"].ToString();
                        DropDownList1.SelectedValue = dt.Rows[0]["state"].ToString().Trim();
                        TextBox6.Text = dt.Rows[0]["city"].ToString();
                        TextBox7.Text = dt.Rows[0]["pincode"].ToString();
                        TextBox5.Text = dt.Rows[0]["full_address"].ToString();
                        TextBox8.Text = dt.Rows[0]["member_id"].ToString();
                        string password = EncryptionDecryption.Decrypt(dt.Rows[0]["password"].ToString().Trim());
                        TextBox11.Text = password.ToString();

                        Label1.Text = dt.Rows[0]["account_status"].ToString();
                        if (dt.Rows[0]["account_status"].ToString().Trim() == "Active")
                        {
                            Label1.Attributes.Add("class", "badge badge-pill text-bg-success");
                        }
                        else if (dt.Rows[0]["account_status"].ToString().Trim() == "Pending")
                        {
                            Label1.Attributes.Add("class", "badge badge-pill text-bg-warning");
                        }
                        else if (dt.Rows[0]["account_status"].ToString().Trim() == "Deactivated")
                        {
                            Label1.Attributes.Add("class", "badge badge-pill  text-bg-danger");
                        }
                        else
                        {
                            Label1.Attributes.Add("class", "badge badge-pill text-bg-info");
                        }

                    }
                }
            }
            catch(Exception ex )
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {

            }
        }

       
    }
}