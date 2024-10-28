using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class adminbookissuing : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        //GO BUTTON
        protected void Button1_Click(object sender, EventArgs e)
        {
                getNames();
        }

        //ISSUE 
        protected void Button2_Click(object sender, EventArgs e)
        {
            if(checkIfBookExists() && checkIfMemberExists())
            {
                if (checkIfIssueEntryExists())
                {
                    Response.Write("<script>alert('This member already has a book')</script>");
                }
                else
                {
                    issueBook();
                }
                
            }
            else
            {
                Response.Write("<script>alert('Enter Valid Book Id or Member Id')</script>");
            }
        }

        //RETURN
        protected void Button6_Click(object sender, EventArgs e)
        {
            if (checkIfBookExists() && checkIfMemberExists())
            {
                if (checkIfIssueEntryExists())
                {
                    returnBook();
                }
                else
                {
                    Response.Write("<script>alert('Enter Valid Book Id or Member Id')</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('Enter Valid Book Id or Member Id')</script>");
            }
        }


        //USER DEFINED FUNCTIONS
        public bool checkIfBookExists()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT * FROM book_master_tbl WHERE book_id = '"+TextBox3.Text.Trim()+ "' AND current_stock > 0 ";
                SqlCommand cmd = new SqlCommand(query, con);    
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if(dt.Rows.Count >= 1)
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
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return false;
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        public bool checkIfMemberExists()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT * FROM member_master_tbl WHERE member_id='"+TextBox2.Text.Trim()+"'";
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
                return false;
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        public bool checkIfIssueEntryExists()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT * FROM book_issue_tbl WHERE member_id='" + TextBox2.Text.Trim() + "' AND book_id = '"+TextBox3.Text.Trim()+"'";
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
                return false;
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        public void getNames()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open(); 
                }
                string query = "SELECT book_name FROM book_master_tbl WHERE book_id = '" + TextBox3.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(query , con);
                SqlDataAdapter da = new SqlDataAdapter( cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count >= 1 )
                {
                    TextBox4.Text = dt.Rows[0]["book_name"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Please Enter A Valid Book Id')</script>");
                }


                string query1 = "SELECT full_name FROM member_master_tbl WHERE member_id = '"+TextBox2.Text.Trim()+"'";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count >= 1)
                {
                    TextBox1.Text = dt1.Rows[0]["full_name"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Please Enter A Valid Member Id')</script>");
                }

            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close() ;   
            }
        }
        public void issueBook()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = "INSERT INTO book_issue_tbl (member_id , member_name , book_id, issue_date , due_date) VALUES (@member_id , @member_name , @book_id , @issue_date , @due_date)";
                SqlCommand cmd = new SqlCommand(query , con);
                cmd.Parameters.AddWithValue("@member_id" , TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@member_name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_id", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@issue_date", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@due_date", TextBox6.Text.Trim());
                cmd.ExecuteNonQuery();

                string query1 = "UPDATE book_master_tbl SET current_stock = current_stock-1 WHERE book_id='"+TextBox3.Text.Trim()+"'";
                SqlCommand cmd1 = new SqlCommand(query1,con);
                cmd1.ExecuteNonQuery();

                Response.Write("<script>alert('Book Issued Successfully')</script>");
                GridView1.DataBind();
            }
            catch(Exception ex)
            {
				Response.Write("<script>alert('Enter a Valid Book Id')</script>");
			}
            finally
            {
                con.Close();
            }
        }
        public void returnBook()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "DELETE FROM book_issue_tbl WHERE book_id = '"+TextBox3.Text.Trim()+"' AND member_id='"+TextBox2.Text.Trim()+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                int result = cmd.ExecuteNonQuery();

                if(result>0)
                {
                    query = "update book_master_tbl set current_stock = current_stock+1 WHERE book_id='" + TextBox3.Text.Trim() + "'";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Book Returned Successfully')</script>");
                    GridView1.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('Enter Valid Book Id or Member Id')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                
            }
            finally
            {
                con.Close();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > dt)
                    {
                        
                        e.Row.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}