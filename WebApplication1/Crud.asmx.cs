using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;
using System.Text;
using System.Threading;
using WebApplication1.Common;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Crud : System.Web.Services.WebService
    {
        common commonClassObj = new common();
        [WebMethod]

        public void CrudOperation(string userData)
        {

            UserData userDetails = new UserData();

            try
            {
                if (!string.IsNullOrEmpty(userData))
                {
                    userDetails = JsonConvert.DeserializeObject<UserData>(userData);
                }
                commonClassObj.setUserDetails(userDetails);
            }
            catch (Exception ex)
            {

            }

        }

        [WebMethod]
        public Object NewUserLogin(string memberId, string password)
        {
            Object userDetails = new Object();

            try
            {
                if (!string.IsNullOrEmpty(memberId) && !string.IsNullOrEmpty(password))
                {
                    userDetails = commonClassObj.newUserLogin(memberId, password);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userDetails;
        }

        [WebMethod]

        public string SetPassword(string memberId, int code, string password)
        {
            string message = "";
            try
            {

                if (!string.IsNullOrEmpty(memberId) && !string.IsNullOrEmpty(password) && code == 1)
                {
                    message = commonClassObj.setPassword(memberId, code, password);
                }
                return message;
            }
            catch (Exception ex)
            {

            }
            return message;
        }

        [WebMethod]
        public string GetData()
        {
            List<UserDetails> userDetails = commonClassObj.GetDetailsAllUsers();
            return JsonConvert.SerializeObject(userDetails);
        }


        [WebMethod]
        public string GetDefaultersData()
        {
            List<DefaultersData> defaulters = commonClassObj.GetDefaulterUser();
            return JsonConvert.SerializeObject(defaulters);

        }

        [WebMethod]

        public void SendMailsToDefaulters(string data)
        {


            if (!string.IsNullOrEmpty(data))
            {
                List<DefaultersData> DataDefaultersList = JsonConvert.DeserializeObject<List<DefaultersData>>(data);
                commonClassObj.sendEmailToDefaulters(DataDefaultersList);
            }

        }


        [WebMethod]
        public bool DeleteDefaulter(string memberId, string bookName)
        {
            bool isDeleted = false;

            if (!string.IsNullOrEmpty(memberId) && !string.IsNullOrEmpty(bookName))
            {
                isDeleted = commonClassObj.DeleteDefaulters(memberId, bookName);
            }
            return isDeleted;
        }

        [WebMethod]
        public bool DeleteSelectedDefaulters(string data)
        {
            bool isDeleted = false;
            if (!string.IsNullOrEmpty(data))
            {
                List<DefaultersData> DataDefaultersList = JsonConvert.DeserializeObject<List<DefaultersData>>(data);
                isDeleted = commonClassObj.DeleteMultipleDefaulters(DataDefaultersList);
            }
            return isDeleted;
        }

        [WebMethod]
        public List<SearchGlobalParameters> SeachItemsGlobal(string SearchTerm)
        {
            List<SearchGlobalParameters> suggestions = new List<SearchGlobalParameters>();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                suggestions = commonClassObj.GlobalSearch(SearchTerm);
            }
            return suggestions;
        }

        [WebMethod]
        public List<DataForGraphBookStock> makeGraphs()
        {
            List<DataForGraphBookStock> data = new List<DataForGraphBookStock>();
            data = commonClassObj.getDataForGraphs();
            return data;

        }

        [WebMethod]
        public string  ValidateAddress(string data)
        {
            string response = "";
            if (!string.IsNullOrEmpty(data))
            {
				ValidateAddressFields AddressFields = JsonConvert.DeserializeObject<ValidateAddressFields>(data);
				response = commonClassObj.ValidateAddress(AddressFields);
				
			}
			return response;
		}
    }
}