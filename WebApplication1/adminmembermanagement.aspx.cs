using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Data.Common;
using System.Data.OleDb;






namespace WebApplication1
{
    
    public static class DataTableExtensions
    {
        //TO CSV
        public static bool toCSV(this DataTable dt, string filePath)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath, false);

                //COLUMNS
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < dt.Columns.Count - 1)
                    {
                        sw.Write(',');
                    }
                }

                sw.Write(sw.NewLine);
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(row[i]))
                        {
                            string value = row[i].ToString();
                            if (value.Contains(","))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(row[i].ToString());
                            }
                        }
                        if (i < dt.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
            return false;
            
        }
    }
    public partial class adminmembermanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        //Go Button
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            if (checkMemberExists())
            {
                getMemberById();
            }
            else
            {
                Response.Write("<script>alert('Member Does Not Exist with this Member ID, try other ID');</script>");
            }
        }

        public void getMemberById()
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
                        TextBox2.Text = dr.GetValue(0).ToString();
                        TextBox7.Text = dr.GetValue(10).ToString();
                        TextBox3.Text = dr.GetValue(2).ToString();
                        TextBox4.Text = dr.GetValue(3).ToString();
                        TextBox9.Text = dr.GetValue(4).ToString();
                        TextBox10.Text = dr.GetValue(5).ToString();
                        TextBox11.Text = dr.GetValue(2).ToString();
                        TextBox6.Text = dr.GetValue(7).ToString();
                        TextBox8.Text = dr.GetValue(1).ToString();
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
        //Active Button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (checkMemberExists())
            {
                updateMemberStatusById("Active");
            }
            else
            {
                Response.Write("<script>alert('Member Does Not Exist with this Member ID, try other ID');</script>");
            }

           
        }
        //Pending
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (checkMemberExists())
            {
                updateMemberStatusById("Pending");
            }
            else
            {
                Response.Write("<script>alert('Member Does Not Exist with this Member ID, try other ID');</script>");
            }
            
        }
        //DEACTIVATE
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (checkMemberExists())
            {
                updateMemberStatusById("Deactivated");
            }
            else
            {
                Response.Write("<script>alert('Member Does Not Exist with this Member ID, try other ID');</script>");
            }
            
        }
        //DELETE US
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Trim().Equals(""))
            {
                Response.Write("<script>alert('Member ID Can Not Be Blank');</script>");
            }
            else
            {
                if (checkMemberExists())
                {
                    deleteUserById();
                }
                else
                {
                    Response.Write("<script>alert('Member Does Not Exist with this Member ID, try other ID');</script>");
                }
            }

        }

        public bool checkMemberExists()
        {
            //int count;
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT * FROM member_master_tbl WHERE member_id = @member_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());

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
            return false;
        }

        public void updateMemberStatusById(string status)
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "UPDATE member_master_tbl SET account_status = '"+status+"' WHERE member_id='"+TextBox1.Text.Trim()+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Status Updated Successfully')</script>");
                TextBox7.Text = status;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                GridView1.DataBind();
                con.Close();
            }
        }

        public void deleteUserById()
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "DELETE FROM member_master_tbl WHERE member_id = '"+TextBox1.Text.Trim()+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Account Deleted Successfully')</script>");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                GridView1.DataBind();
                con.Close();
                clearForm();
            }
        }
        public void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox7.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox6.Text = "";
            TextBox8.Text = "";
        }

        

        

        //DOWNLOAD DATA

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            DataTable dt = new DataTable();
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT full_name AS NAME, dob AS DOB, contact_no AS ContactNumber, email AS Email, state  As State, city AS City , pincode AS PinCode , full_address AS Address, member_id AS MemberId , account_status AS Status FROM member_master_tbl;";
                SqlCommand cmd = new SqlCommand(query , con);
                
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                
                string path = "";
                
                Thread t = new Thread((ThreadStart)(() => {
                    SaveFileDialog openFileDialog = new SaveFileDialog();

                    // Set properties for the open dialog
                    string filename = "memberData_" + SanitizeFileName(DateTime.Now.ToString("dd/MM/yyyy"))+".csv";

                    openFileDialog.Filter = "CSV Files|*.csv";
                    openFileDialog.Title = "Save File As";
                    openFileDialog.FileName = filename;
                    // Show the dialog and wait for user input
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        path = openFileDialog.FileName;
                        if (dt.toCSV(path))
                        {
                            Response.Write("<script>alert('File Downloaded')</script>");
                        }
                        
                    }
                    else
                    {

                        Response.Write("<script>alert('Please specify a path')</script>");
                        path = null;
                    }
                    
                }));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();
                
                con.Close();
            }
        }
        public string SanitizeFileName(string fileName)
        {
            foreach (char invalidChar in System.IO.Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidChar, '_');
            }
            return fileName;
        }
        //Upload Data
        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                DataTable tblcsv = new DataTable();
                tblcsv.Columns.Add("full_name");
                tblcsv.Columns.Add("dob");
                tblcsv.Columns.Add("contact_no");
                tblcsv.Columns.Add("email");
                tblcsv.Columns.Add("state");
                tblcsv.Columns.Add("city");
                tblcsv.Columns.Add("pincode");
                tblcsv.Columns.Add("full_address");
                tblcsv.Columns.Add("member_id");
                tblcsv.Columns.Add("account_status");
                string filename = Path.GetFileName(FileUpload1.FileName);
                string filepath = Server.MapPath("~/DownloadedData/" + filename);
                FileUpload1.SaveAs(filepath);
                //string filepath = Path.GetFullPath(FileUpload1.PostedFile.FileName);
                string ReadCSV = File.ReadAllText(filepath);
                
                foreach(string csvRow in ReadCSV.Split('\n')) {
                    if(!string.IsNullOrEmpty(csvRow))
                    {
                        tblcsv.Rows.Add();
                        int count = 0;
                        foreach (string FileRec in csvRow.Split(','))
                        {
                            tblcsv.Rows[tblcsv.Rows.Count - 1][count] = FileRec;
                            count++;
                        }
                    } 
                }
                InsertCSVRecords(tblcsv);
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert(''" + ex.Message + "'')</script>");
            }
        }

        public void InsertCSVRecords(DataTable csvdt)
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            try
            {
                if(con.State == ConnectionState.Closed )
                {
                    con.Open();
                }
                objbulk.BulkCopyTimeout = 600;
                objbulk.DestinationTableName = "member_master_tbl";
                objbulk.ColumnMappings.Add("full_name", "full_name");
                objbulk.ColumnMappings.Add("dob", "dob");
                objbulk.ColumnMappings.Add("contact_no", "contact_no");
                objbulk.ColumnMappings.Add("email", "email");
                objbulk.ColumnMappings.Add("state", "state");
                objbulk.ColumnMappings.Add("city", "city");
                objbulk.ColumnMappings.Add("pincode", "pincode");
                objbulk.ColumnMappings.Add("full_address", "full_address");
                objbulk.ColumnMappings.Add("member_id", "member_id");
                objbulk.ColumnMappings.Add("account_status", "account_status");
                objbulk.WriteToServer(csvdt);
                Response.Write("<script>alert('Data Exported To SQlServer')</script>");
                con.Close();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert(''" + ex.Message + "'')</script>");
            }

        }

    }
}