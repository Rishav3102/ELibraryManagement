using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Drawing;

namespace WebApplication1
{
    public partial class adminbookinventory : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath;
        static int global_actual_stock, global_current_stock, global_issued_books;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            if (!IsPostBack)
            {
                fillAuthorPublisherValue();
            }
            
        }
        //ADD
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkIfBookExists())
            {
                Response.Write("<script>alert('Book Already Exist with this Book ID, try other ID');</script>");
            }
            else
            {
                addNewBook();
            }
        }

        //GO
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfBookExists())
            {
                getBookById();
            }
            else
            {
                Response.Write("<script>alert('Book Does Not Exist with this Book ID, try other ID');</script>");

            }
        }

        //UPDATE
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfBookExists())
            {
                updateBookById();
            }
            else
            {
                Response.Write("<script>alert('Book Does Not Exist with this Book ID, try other ID');</script>");

            }
        }

        //DELETE
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfBookExists())
            {
                deleteBookById();
            }
            else
            {
                Response.Write("<script>alert('Book Does Not Exist with this Book ID, try other ID');</script>");

            }
        }

        public void deleteBookById()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "DELETE FROM book_master_tbl WHERE book_id = '" + TextBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Book Deleted Successfully');</script>");
                GridView1.DataBind();

            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close();    
            }
        }
        public void updateBookById()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                int actual_stock = Convert.ToInt32(TextBox4.Text.Trim());
                int current_stock = Convert.ToInt32(TextBox5.Text.Trim());

                if(global_actual_stock == actual_stock)
                {

                }
                else
                {
                    if(actual_stock < global_issued_books)
                    {
                        Response.Write("<script>alert('Actual Stock Value can not be less than the Issued books');</script>");
                        return;
                    }
                    else
                    {
                        current_stock = actual_stock-global_issued_books;
                        TextBox5.Text = "" + current_stock;
                    }
                }
                string genres = "";
                foreach(int i in ListBox1.GetSelectedIndices())
                {
                    genres += ListBox1.Items[i] + ",";
                }
                genres = genres.Remove(genres.Length - 1);
                string filepath = "~/book_inventory/books1";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if(filename == "" || filename == null)
                {
                    filepath = global_filepath;
                }
                else
                {
                    FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                    filepath = "~/book_inventory/" + filename;
                }

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = "UPDATE book_master_tbl SET book_id = @book_id , book_name = @book_name , genre = @genre , author_name = @author_name , publisher_name = @publisher_name , publisher_date = @publisher_date , language= @language , edition= @edition , book_cost = @book_cost , no_of_pages = @no_of_pages , book_description = @book_description, actual_stock = @actual_stock , current_stock = @current_stock, book_img_link = @book_img_link WHERE book_id='" + TextBox1.Text.Trim()+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@book_id" , TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", DropDownList3.SelectedItem.Value.ToString());
                cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value.ToString());
                cmd.Parameters.AddWithValue("@publisher_date", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@book_description", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", actual_stock.ToString());
                cmd.Parameters.AddWithValue("@current_stock", current_stock.ToString());
                cmd.Parameters.AddWithValue("@genre", genres);
                cmd.Parameters.AddWithValue("@book_img_link", filepath);

                cmd.ExecuteNonQuery();
                
                Response.Write("<script>alert('Book Details Updated Successfully');</script>");
                GridView1.DataBind();

            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close();
            }
        }
        public bool checkIfBookExists()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
               
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT * FROM book_master_tbl WHERE book_id = '" + TextBox1.Text.Trim() + "'";
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
                con.Close();
            }

            return false;
        }

        public void addNewBook()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                List<string> listItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(Newtonsoft.Json.JsonConvert.SerializeObject(ListBox1.Items));
                
                var indexList = ListBox1.GetSelectedIndices();
                
                var list = listItems.Select((item, index) => {
                    string listItem = "";
                    if (indexList.Contains(index))
                    {
                        listItem = item;
                    }
                    return listItem;
                }).Where(x=> !string.IsNullOrEmpty(x)).ToList();
                string commaSepString = String.Join(",", list);

                string filepath = "~/book_inventory/books1.png";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("book_inventory/" + filename));
                filepath = "~/book_inventory/" + filename;
                
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "INSERT INTO book_master_tbl(book_id , book_name , genre , author_name , publisher_name , publisher_date , language , edition , book_cost , no_of_pages , book_description , actual_stock , current_stock , book_img_link)VALUES(@book_id, @book_name, @genre , @author_name , @publisher_name , @publisher_date , @language , @edition , @book_cost , @no_of_pages , @book_description , @actual_stock , @current_stock , @book_img_link)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", commaSepString);
                cmd.Parameters.AddWithValue("@author_name", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisher_date", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@book_description", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@current_stock", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@book_img_link", filepath);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Book Details Added Successfully')</script>");

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

        public void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
        }
        

        public void fillAuthorPublisherValue()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT author_name FROM author_master_tbl";
                SqlCommand cmd = new SqlCommand(query,con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DropDownList3.DataSource = dt;
                DropDownList3.DataValueField = "author_name";
                DropDownList3.DataBind();

                string query1 = "SELECT publisher_name FROM publisher_master_tbl";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                DropDownList2.DataSource = dt1;
                DropDownList2.DataValueField = "publisher_name";
                DropDownList2.DataBind();

            }
            catch(Exception ex){
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close() ;
            }
        }
        

        public void getBookById()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                
                if(con.State == ConnectionState.Closed) {
                    con.Open();
                }
                string query = "SELECT * FROM book_master_tbl WHERE book_id = @book_id";
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count >= 1) {
                    TextBox2.Text = dt.Rows[0]["book_name"].ToString().Trim();
                    DropDownList1.SelectedValue = dt.Rows[0]["language"].ToString().Trim();
                    DropDownList2.SelectedValue = dt.Rows[0]["publisher_name"].ToString().Trim();
                    DropDownList3.SelectedValue = dt.Rows[0]["author_name"].ToString().Trim();
                    TextBox3.Text = dt.Rows[0]["publisher_date"].ToString().Trim();
                    ListBox1.ClearSelection();
                    string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                    for(int i = 0; i< genre.Length; i++)
                    {
                        for(int j = 0; j<ListBox1.Items.Count; j++)
                        {
                            if (ListBox1.Items[j].ToString() == genre[i])
                            {
                                ListBox1.Items[j].Selected = true;
                            }
                        }
                    }
                    TextBox9.Text = dt.Rows[0]["edition"].ToString().Trim();
                    TextBox10.Text = dt.Rows[0]["book_cost"].ToString().Trim();
                    TextBox11.Text = dt.Rows[0]["no_of_pages"].ToString().Trim();
                    TextBox4.Text = dt.Rows[0]["actual_stock"].ToString().Trim();
                    TextBox5.Text = dt.Rows[0]["current_stock"].ToString().Trim();
                    TextBox7.Text = "" + (Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString()) - Convert.ToInt32(dt.Rows[0]["current_stock"].ToString())) + "";
                    TextBox6.Text = dt.Rows[0]["book_description"].ToString().Trim();

                    global_actual_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                    global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                    global_issued_books = global_actual_stock - global_current_stock;
                    global_filepath = dt.Rows[0]["book_img_link"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Book ID')</script>");
                }
                
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close();
            }
        }

        
    }
}