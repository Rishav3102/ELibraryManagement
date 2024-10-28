using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Net.Http;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows.Controls.Primitives;
using System.ServiceModel.Channels;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Common;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace WebApplication1.Common
{
    public enum userLoginFirstTime
    {
        FirstTime = 1,
        ManyTimes = 2,
    }
    public class UserData
    {
        public string username { get; set; }
        public string dob { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public string address { get; set; }
        public string userId { get; set; }
    };

    public class UserDetails : UserData
    {
        public string AccountStatus {  get; set; }
    };

    public class DefaultersData {
        public string memberId { get; set; }
        public string memberName { get; set; }
        public string bookName { get; set; }
        public string issueDate { get; set; }
        public string dueDate { get; set; }
        public string email { get; set; }
        public int lateFees { get; set; }
    };

    public class SearchGlobalParameters
    {
        public string Name { get; set; }
        public string Type { get; set; }


    };

    public class DataForGraphBookStock
    {
        public int BookId { get; set; }
        public int ActualStock { get; set; }
        public int BookIssued {  get; set; }
    };

    public class ValidateAddressFields
    {
		public string addressLine1 {  get; set; }
		public string addressLine2 { get; set; }
		public string addressCity { get; set; }
		public string addressPostalOrZip { get; set; }
		public string addressProvinceOrState { get; set; }
		public string addressCountry { get; set; }
	}
    public class common
    {
        //PASSWORD GENERATION
        public string pass()
        {
            string randomString;
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
            {
                int index = random.Next(characters.Length);
                stringBuilder.Append(characters[index]);
                randomString = stringBuilder.ToString();

            }
            randomString = stringBuilder.ToString();
            return EncryptionDecryption.Encrypt(randomString);
        }

        //ADDING DETAILS
        public void setUserDetails(UserData userData)
        {
            string username = userData.username;
            string dob = userData.dob;
            string contactNumber = userData.contactNumber;
            string email = userData.email;
            string state = userData.state;
            string city = userData.city;
            string pincode = userData.pincode;
            string address = userData.address;
            string userId = userData.userId;

            string password = pass();

            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "INSERT INTO member_master_tbl(full_name , dob , contact_no , email , state , city , pincode , full_address , member_id , password , account_status , loginFirstTime)VALUES(@full_name , @dob , @contact_no , @email , @state , @city , @pincode , @full_address , @member_id , @password ,  @account_status , @loginFirstTime)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@full_name", username);
                cmd.Parameters.AddWithValue("@dob", dob);
                cmd.Parameters.AddWithValue("@contact_no", contactNumber);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@pincode", pincode);
                cmd.Parameters.AddWithValue("@full_address", address);
                cmd.Parameters.AddWithValue("@member_id", userId);
                cmd.Parameters.AddWithValue("@account_status", "pending");
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@loginFirstTime", userLoginFirstTime.FirstTime);
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                sendEmail(email, password);
                con.Close();
            }
        }

        //EMAIL
        public void sendEmail(string email, string password)
        {
            string currentUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            string setPasswordLink = $"{currentUrl}/newUserLogin.aspx";
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
                                                <span>" + EncryptionDecryption.Decrypt(password) + @"</span>

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

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }



        //LOGIN
        public Object newUserLogin(string memberId, string password)
        {
            int code = -100;
            string message = "";
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                string query = "SELECT * FROM member_master_tbl where member_id = @member_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@member_id", memberId);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        string encryptedPass = dr.GetValue(9).ToString().Trim();
                        string decryptedPass = EncryptionDecryption.Decrypt(encryptedPass).Trim();
                        if (decryptedPass == password)
                        {
                            dr.Close();
                            query = "SELECT loginFirstTime FROM member_master_tbl WHERE member_id = @member_id";
                            cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@member_id", memberId);
                            code = (int)cmd.ExecuteScalar();
                            message = "Login Successfull";
                            return new { memberId = memberId, code = code, message = message };
                        }
                        else
                        {
                            message = "Invalid Credentials";
                        }

                    }
                }
                else
                {
                    message = "Invalid Credentials";

                }
                return new { memberId = memberId, code = code, message = message };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public string setPassword(string memberId, int code, string password)
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string PassFromDb = "";
            string message = "";
            bool PasswordResetSuccessfull = false;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string query = "SELECT password FROM member_master_tbl WHERE member_id = '" + memberId + "'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count == 1)
                            {
                                PassFromDb = EncryptionDecryption.Decrypt(dt.Rows[0]["password"].ToString());
                                if (PassFromDb != password)
                                {
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }
                                    query = "UPDATE member_master_tbl SET password = '" + EncryptionDecryption.Encrypt(password) + "' WHERE member_id='" + memberId + "'";
                                    using (SqlCommand sq = new SqlCommand(query, con))
                                    {
                                        sq.ExecuteNonQuery();
                                        message = "Password Reset Successfull";
                                        PasswordResetSuccessfull = true;
                                    }
                                }
                                else
                                {
                                    message = "New Password Can Not Be Equal To Previous Password";
                                }
                            }
                        }
                    }
                }
                if (PasswordResetSuccessfull)
                {
                    using (SqlConnection con1 = new SqlConnection(strcon))
                    {
                        int codeNew = Convert.ToInt32(userLoginFirstTime.ManyTimes);
                        string query = "UPDATE member_master_tbl SET loginFirstTime = '" + codeNew + "' WHERE member_id='" + memberId + "'";
                        using (SqlCommand cmd = new SqlCommand(query, con1))
                        {
                            if (con1.State == ConnectionState.Closed)
                            {
                                con1.Open();
                            }
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                return message;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return message;
        }

        public List<UserDetails> GetDetailsAllUsers()
        {
            List<UserDetails> users = new List<UserDetails>();
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string query = "SELECT full_name , dob , contact_no , email , state , city , pincode , full_address , member_id , account_status FROM member_master_tbl";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            UserDetails userDetails = new UserDetails
                            {
                                username = dr["full_name"].ToString(),
                                dob = dr["dob"].ToString(),
                                contactNumber = dr["contact_no"].ToString(),
                                email = dr["email"].ToString(),
                                state = dr["state"].ToString(),
                                city = dr["city"].ToString(),
                                pincode = dr["pincode"].ToString(),
                                address = dr["full_address"].ToString(),
                                userId = dr["member_id"].ToString(),
                                AccountStatus = dr["account_status"].ToString(),
                            };

                            users.Add(userDetails);
                        }
                    }
                }
            }
            return users;
        }

        //GET DEFAULTER USER FROM DB
        public List<DefaultersData> GetDefaulterUser()
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            List<DefaultersData> defaulters = new List<DefaultersData>();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "EXEC SP_FIND_LATE_FEE_CANDIDATES";
                    using (SqlCommand sc = new SqlCommand(query, con))
                    {
                        using (SqlDataReader dr = sc.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                DefaultersData defaulter = new DefaultersData
                                {

                                    memberId = dr["member_id"].ToString(),
                                    memberName = dr["member_name"].ToString(),
                                    bookName = dr["book_name"].ToString(),
                                    issueDate = dr["issue_date"].ToString(),
                                    dueDate = dr["due_date"].ToString(),
                                    email = dr["email"].ToString(),
                                    lateFees = Convert.ToInt32(dr["LATE_FEES"])
                                };

                                defaulters.Add(defaulter);
                            }
                        }
                    }
                }
                return defaulters;

            }


            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return defaulters;
        }


        //SEND MAILS TO DEFAULTERS
        public void sendEmailToDefaulters(List<DefaultersData> DataDefaultersList)
        {

            foreach (var Defaulter in DataDefaultersList)
            {
                sendEmail(Defaulter.email, Defaulter.memberName, Defaulter.bookName, Defaulter.dueDate, Defaulter.lateFees);
            }
        }


        public void sendEmail(string email, string memberName, string bookName, string dueDate, int lateFees)
        {

            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "rishav@xorosoft.com";
            string smtpPassword = "kzdo ygry ccdx dfxq";
            SmtpClient smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = "You Have Not Returned the Book So please Return the book",
                IsBodyHtml = true,
            };

            string emailBody = @"
                                <!DOCTYPE html>
                                <html lang='en'>
                                <head>
                                    <meta charset='UTF-8'>
                                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                    <title>Please Return Book</title>
                                </head>
                                <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
                                    <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px; margin: 0 auto; background-color: #ffffff; box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.3);'>
                                        <tr>
                                            <td align='center' valign='top' style='padding: 20px 0;'>
                                                <h1 style='color: #007bff; margin-bottom: 20px;'>Please Return Book</h1>
                                                <p>Dear User,</p>
                                                <table border='0' cellpadding='0' cellspacing='0' style='width: 100%;'>
                                                    <tr>
                                                        <td align='center' style='padding: 20px;'>
                                                           <b>Hi " + memberName + @" Please return book " + bookName + @" as its due date was " + dueDate + @" and don't forget to pay the late fees of rupees " + lateFees + @"</b>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <p>If you didn't request this password , please ignore this email.</p>
                                                <p style='color: #888; font-size: 12px;'>This email was sent to you as part of a password reset request. If you have any concerns, please contact our support team.</p>
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

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        public bool DeleteDefaulters(string memberId, string bookName)
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "EXEC SP_DELETE_DEFAULTERS @BOOK_NAME = '" + bookName + "', @MEMBER_ID = '" + memberId + "'";
                    ;
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteMultipleDefaulters(List<DefaultersData> DataDefaultersList)
        {
            try
            {
                foreach (var defaulter in DataDefaultersList)
                {
                    DeleteDefaulters(defaulter.memberId, defaulter.bookName);
                }
                return true;
            }
            catch { return false; }

        }

        public List<SearchGlobalParameters> GlobalSearch(string SearchTerm)
        {
            List<SearchGlobalParameters> suggestions = new List<SearchGlobalParameters>();
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }


                    string query = "SELECT full_name , 'MemberName' as Type FROM member_master_tbl WHERE full_name LIKE @SearchTerm UNION SELECT author_name, 'AutherName' as Type FROM author_master_tbl WHERE author_name LIKE @SearchTerm UNION SELECT book_name, 'BookName' as Type FROM book_master_tbl WHERE book_name LIKE @SearchTerm UNION SELECT PAGE_NAME , 'Page' AS TYPE FROM PAGES WHERE PAGE_NAME LIKE @SearchTerm";



                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (SearchTerm.Length == 1)
                        {
                            cmd.Parameters.AddWithValue("@SearchTerm", SearchTerm + "%");

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@SearchTerm", "%" + SearchTerm + "%");

                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SearchGlobalParameters suggestion = new SearchGlobalParameters
                                {
                                    Name = reader["full_name"].ToString(),
                                    Type = reader["Type"].ToString(),
                                };
                                suggestions.Add(suggestion);
                            }
                        }
                    }
                }
                return suggestions;
            }
            catch (Exception ex)
            {
                return suggestions;
            }
        }

        public List<DataForGraphBookStock> getDataForGraphs()
        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            List<DataForGraphBookStock> data = new List<DataForGraphBookStock>();
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT book_id , current_stock  , (CAST(actual_stock as INT)- CAST(current_stock as INT)) as BookIssued FROM book_master_tbl";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DataForGraphBookStock currData = new DataForGraphBookStock
                                {
                                    BookId = Convert.ToInt32(reader["book_id"]),
                                    ActualStock = Convert.ToInt32(reader["current_stock"]),
                                    BookIssued = Convert.ToInt32(reader["BookIssued"])

                                };
                                data.Add(currData);
                            }
                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                return data;
            }


        }


        public string ValidateAddress(ValidateAddressFields data)
        {

			
            string apiUrl = "https://api.postgrid.com/v1/addver/suggestions?includeDetails=true";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "POST";

            request.Headers.Add("x-api-key", "live_sk_emCUnKFjTDyJTi44qegzfq");

            string postData = $"address[line1] = {data.addressLine1}" +
						  $"&address[line2] = {data.addressLine2}" +
                          $"&address[city] = {data.addressCity}" +
						  $"&address[postalOrZip] = {data.addressPostalOrZip}" +
						  $"&address[provinceOrState] = {data.addressProvinceOrState}" +
                          $"&address[country] = {data.addressCountry}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }


			using (WebResponse response = request.GetResponse())
			{
				
				using (Stream responseStream = response.GetResponseStream())
				{
					using (StreamReader reader = new StreamReader(responseStream))
					{
						string responseText = reader.ReadToEnd();
                        return responseText;
					}
				}
			}
		}


    }
}


