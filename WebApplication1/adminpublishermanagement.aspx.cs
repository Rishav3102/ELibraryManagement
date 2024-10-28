using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication1
{
    public partial class adminpublishermanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //ADD
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfPublisherExists())
            {
                Response.Write("<script>alert('Publisher Already Exist with this Author ID, try other ID');</script>");
            }
            else
            {
                signUpNewPublisher();
            }
        }

        public bool checkIfPublisherExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT * FROM publisher_master_tbl WHERE publisher_id = '" + TextBox3.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {

            }

            return false;
        }

        public void signUpNewPublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "INSERT INTO publisher_master_tbl (publisher_id , publisher_name) VALUES (@publisherId , @publisherName)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@publisherId", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@publisherName", TextBox2.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author Details Added Successfully')</script>");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                clearForm();
                GridView1.DataBind();
            }
        }

        void clearForm()
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
        }
        //UPDATE
        protected void Button6_Click(object sender, EventArgs e)
        {
            if (checkIfPublisherExists())
            {
                updatePublisher();
            }
            else
            {
                Response.Write("<script>alert('Publisher Does Not Exist with this Publisher ID, try other ID');</script>");
            }
        }

        public void updatePublisher()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "UPDATE publisher_master_tbl SET publisher_name = @publisher_name WHERE publisher_id = @publisher_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_id", TextBox3.Text.Trim());
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Publisher Details Updates Successfully')</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close();
                clearForm();
                GridView1.DataBind();
            }
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            if (checkIfPublisherExists())
            {
                deletePublisher();
            }
            else
            {
                Response.Write("<script>alert('Publisher Does Not Exist with this Author ID, try other ID');</script>");
            }
        }

        public void deletePublisher()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "DELETE FROM publisher_master_tbl WHERE publisher_id = @publisher_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@publisher_id", TextBox3.Text.Trim());
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Publisher Deleted Successfully')</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close();
                clearForm();
                GridView1.DataBind();
            }
        }
        //GO
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkIfPublisherExists())
            {
                getPublisherById();
            }
            else
            {
                Response.Write("<script>alert('Publisher Does Not Exist with this Author ID, try other ID');</script>");
            }
        }

        public void getPublisherById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT * FROM publisher_master_tbl WHERE publisher_id = '" + TextBox3.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Publisher Does Not Exist with this Author ID, try other ID');</script>"); ;
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