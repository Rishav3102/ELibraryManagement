using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace WebApplication1
{
    public partial class adminauthormanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        //ADD
        protected void Button2_Click(object sender, EventArgs e)
        {
            

            if (checkIfAuthorExists())
            {
                Response.Write("<script>alert('Author Already Exist with this Author ID, try other ID');</script>");
            }
            else
            {
                signUpNewAdmin();
            }

        }
        //Update
        protected void Button6_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                updateAuthor();
            }
            else
            {
                Response.Write("<script>alert('Author Does Not Exist with this Author ID, try other ID');</script>");
            }
        }
        //Delete
        protected void Button10_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                deleteAuthor();
            }
            else
            {
                Response.Write("<script>alert('Author Does Not Exist with this Author ID, try other ID');</script>");
            }
        }
        //go
        protected void Button1_Click(object sender, EventArgs e)
        {
            if(checkIfAuthorExists())
            {
                getAuthorById();
            }
            else
            {
                Response.Write("<script>alert('Author Does Not Exist with this Author ID, try other ID');</script>");
            }
        }

        public void getAuthorById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT * FROM author_master_tbl WHERE author_id = '" + TextBox3.Text.Trim() + "'";
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
                    Response.Write("<script>alert('Author Does Not Exist with this Author ID, try other ID');</script>"); ;
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
        public bool checkIfAuthorExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT * FROM author_master_tbl WHERE author_id = '" + TextBox3.Text.Trim() + "'";
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
            catch(Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"')</script>");
            }
            finally
            {

            }
            
            return false;
        }

        public void signUpNewAdmin()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "INSERT INTO author_master_tbl (author_id , author_name) VALUES (@authorId , @authorName)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@authorId", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@authorName", TextBox2.Text.Trim());
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

        public void updateAuthor()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "UPDATE author_master_tbl SET author_name = @author_name WHERE author_id = @author_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@author_name" , TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@author_id" , TextBox3.Text.Trim());
                cmd.ExecuteNonQuery ();
                Response.Write("<script>alert('Author Details Updates Successfully')</script>");
            }
            catch(Exception ex )
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

        public void deleteAuthor()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "DELETE FROM author_master_tbl WHERE author_id = @author_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@author_id", TextBox3.Text.Trim());
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Author Deleted Successfully')</script>");
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

        void clearForm()
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
        }
    }
}