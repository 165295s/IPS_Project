using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.UI;
using System.Web.Configuration;
using IPS_Prototype.Class;
using IPS_Prototype.Model;
using IPS_Prototype.RetrieveClass;
using System.Collections;

namespace IPS_Prototype.DAL
{
    public class DatabaseDAO
    {
        bool hasAccess = true;
        bool pageFound = false;
        DbHelper dbhelp = new DbHelper();
        static List<Object> paIdList = new List<Object>();
        

        //START OF MASTERPAGE METHODS
        public bool AccessRight(string role, string currentPageName)
        {
            try
            {


                // To check user's access right
                // We need to validate against pageList.json
                // To check if the page user wants to visit
                // Is listed in the json file
                if (currentPageName != "UsersLogin" && currentPageName != "Default")
                {
                    // read JSON directly from a file
                    using (StreamReader file = new StreamReader(HttpContext.Current.Server.MapPath("./Json/pageList.json")))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject o2 = (JObject)JToken.ReadFrom(reader);

                        var accessRight = o2["accessRight"];
                        JToken pages = null;
                        foreach (var i in accessRight)
                        {
                            var roles = i["role"].ToString();
                            if (roles == role)
                            {
                                pages = i["page"];
                                break;
                            }
                        }

                        foreach (var p in pages)
                        {
                            var individualPage = p;
                            if (p.ToString() == currentPageName)
                            {
                                hasAccess = true;
                                pageFound = true;
                                break;
                            }
                        }

                        if (pageFound == false)
                        {
                            hasAccess = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }
            return hasAccess;
        }
        //END OF MASTERPAGE METHODS

        //START OF MANAGE USERS METHODS
        //To retrieve all User data to display in User_Management.aspx table
        public DataTable GetUsers()
        {

            string commandtext = "SELECT User_Id As user_id, User_Name AS user_name, Full_Name AS full_name, Email_Addr AS email_addr, Created_DT As created_date, Modified_DT As modified_Date, Modified_By As modified_by, Role_Type As role_type, Last_Login_DT As last_login_date FROM admin.TBL_USERS;";
            DataTable dt = dbhelp.ExecDataReader(commandtext);

            return dt;
        }

        //public DataTable GetPAInfo(string caRepID) {
        //    List<SqlCommand> transcommand = new List<SqlCommand>();
        //    SqlCommand mycmd = new SqlCommand();
        //    string commandtext = "  Select pa.Honorific as Honorific,pa.first_name as First_Name,pa.last_name as Surname, Pa.Tel_Num as Telephone from Personal_Assistant PA INNER JOIN  Ca_Rep_PA crp on pa.PA_Id = crp.PA_Id INNER JOIN Org_Ca_Rep OCR on OCR.CA_Rep_Id = crp.CA_Rep_Id INNER JOIN Organisation org on org.Org_Id = OCR.Org_Id where OCR.CA_Rep_Id = @caRepId;";

        //    DataTable dt = dbhelp.ExecDataReader(commandtext, "@caRepId",caRepID);

        //    return dt;

        //}

        //public DataTable GetCAREP() {
        //    string commandtext = "Select p.person_id as Person_Id,ca.ca_rep_id as CA_Rep_Id,ca.Org_Id as Org_Id ,p.Honorific AS Honorific,p.First_Name as First_Name, p.Last_Name as Surname, p.Designation_1 as Designation,p.Nationality as Nationality,ca.Role as Role From person p INNER JOIN Org_Ca_Rep CA  on p.Person_Id = ca.Person_Id where ca.Org_Id = (Select org_id from Organisation where Org_Id =(Select Max(org_id) from Organisation));";
        //    DataTable dt = dbhelp.ExecDataReader(commandtext);
           

        //    return dt;

        //}
        //public DataTable GetPAInfo()
        //{
        //    string commandtext = "Select p.person_id as Person_Id,ca.ca_rep_id as CA_Rep_Id,ca.Org_Id as Org_Id ,p.Honorific AS Honorific,p.First_Name as First_Name, p.Last_Name as Surname, p.Designation_1 as Designation,p.Nationality as Nationality,ca.Role as Role From person p INNER JOIN Org_Ca_Rep CA  on p.Person_Id = ca.Person_Id where ca.Org_Id = (Select org_id from Organisation where Org_Id =(Select Max(org_id) from Organisation));";
        //    DataTable dt = dbhelp.ExecDataReader(commandtext);


        //    return dt;

        //}

        //public DataTable GetIndivPAInfo()
        //{
        //    string commandtext = "SELECT Honorific AS Honorific, First_Name AS First_Name, Last_Name AS Surname, Email_Addr AS Email_Addr, Tel_Num AS Tel_Num from Personal_Assistant; ";
        //    DataTable dt = dbhelp.ExecDataReader(commandtext);


        //    return dt;

        //}



        //To create new user in User_Add.aspx
        public int AddUser(string user_name, string full_name, string email_addr, DateTime created_date, string role_type)
        {

            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();

                string commandtext = "INSERT INTO admin.TBL_USERS (User_Name, Full_Name, Email_Addr, Created_DT, Role_Type) VALUES (@user_name, @full_name, @email_addr, @created_date, @role_type);";
                

            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text,"@user_name", user_name, "@full_name", full_name, "@email_addr", email_addr, "@created_date", created_date, "@role_type", role_type);

            transcommand.Add(mycmd);

            result = dbhelp.ExecTrans(transcommand);

            
            }
            catch(Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }

            return result;
        }



        //This method is used 2 times once in Login.cs and the other in User_add.cs
        //To retrieve User Data based on the UserID input in Login.aspx
        //To retrieve selected User data based on the selected row in User_Management.aspx
        //The parameter used to identify the selected row is the UserID
        public UserAddInfo GetData(string UserID)
        {
            UserAddInfo user = new UserAddInfo();
            DataTable dt;
            string commandtext = "SELECT Full_Name, Email_Addr, Role_Type FROM admin.TBL_USERS WHERE User_Name = @user_name";
            dt = dbhelp.ExecDataReader(commandtext, "@user_name", UserID);
            user.Name = dt.Rows[0]["Full_Name"].ToString();
            user.Email = dt.Rows[0]["Email_Addr"].ToString();
            user.Role = dt.Rows[0]["Role_Type"].ToString();
            return user;
        }

        //To retrieve data based on the UserID value on keyup from User_Add.aspx
        //The parameter value comes from textbox id User_Input_Username
        //If method returns a row count of more than 0, this means that the UserID already exists
        public int checkIDValidity(string IDVal)
        {
            int result = 0;
            string commandtext = "Select User_Name FROM admin.TBL_USERS Where User_Name = @user_name;";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@user_name", IDVal.Trim());

            if(dt.Rows.Count > 0)
            {
                result = 0;
            }

            else
            {
                result = 1;
            }

            return result;

        }

        //To edit selected User data based on the current UserID in User_Add.aspx
        //Parameter of current UserID is oldusername
        public int EditUser(string name, string email, string role, string username, string oldusername, DateTime modified)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();

            string commandtext = "UPDATE admin.TBL_USERS SET Full_Name = @full_name, Email_Addr = @email_addr, Role_Type = @role_type, User_Name = @user_name, Modified_DT = @modified_date WHERE User_Name = @username1;";

            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@full_name", name, "@email_addr", email, "@role_type", role, "@user_name", username, "@modified_date", modified, "@username1", oldusername);

            transcommand.Add(mycmd);

            result = dbhelp.ExecTrans(transcommand);

            return result;
        }

        //To delete User data based on the UserID in User_Management.aspx
        //Parameter of UserID is username
        public int DeleteUser(string username)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();

            string commandtext = "DELETE FROM admin.TBL_USERS WHERE User_Name = @username;";

            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@username", username);

            transcommand.Add(mycmd);

            result = dbhelp.ExecTrans(transcommand);

            return result;
        }
        //END OF MANAGE USERS METHODS


        //START OF CODE MANAGEMENT METHODS
        //To retrieve all CodeLookUp data to display in Maintainence_Management.aspx table
        public DataTable GetLookup()
        {

            string commandtext = "SELECT Code_Type, Code, Code_Desc FROM admin.TBL_CODE_LOOKUP;";
            DataTable dt = dbhelp.ExecDataReader(commandtext);
            return dt;
        }

        public DataTable GetLookupSearch(string search)
        {

            string commandtext = "SELECT Code,Code_Desc FROM admin.TBL_CODE_LOOKUP WHERE Code_Type = @search";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@search", search);
            return dt;
        }

        //To create new user in Maintainence_Add.aspx
        public int AddCode(string type, string lookup, string codedesc)
        {

            int result = 0;
            try
            {
                List<SqlCommand> transcommand = new List<SqlCommand>();
                SqlCommand mycmd = new SqlCommand();

                string commandtext = "INSERT INTO admin.TBL_CODE_LOOKUP (Code_Type, Code, Code_Desc) VALUES (@type, @lookup, @codedesc);";

                mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@type", type, "@lookup", lookup, "@codedesc", codedesc);

                transcommand.Add(mycmd);

                result = dbhelp.ExecTrans(transcommand);


            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog(ex.ToString());
            }

            return result;
        }

        //To retrieve data based on the Lkup value on keyup from Maintainence_Add.aspx
        //The parameter value comes from textbox id LkUpCode 
        //If method returns a row count of more than 0, this means that the LkUpCode already exists
        public int checkLookUpValidity(string IDVal, string type)
        {
            int result = 0;
            string commandtext = "Select Code from admin.TBL_CODE_LOOKUP Where Code = @lookup AND Code_Type = @type";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@lookup", IDVal.Trim(), "@type", type.Trim());

            if (dt.Rows.Count > 0)
            {
                result = 0;
            }

            else
            {
                result = 1;
            }

            return result;

        }

        //To retrieve data based on the code description value on keyup from Maintainence_Add.aspx
        //The parameter value comes from textbox id CodeDesc
        //If method returns a row count of more than 0, this means that the code type already exists
        public int checkCodeDescValidity(string IDVal, string type)
        {
            int result = 0;
            string commandtext = "Select Code_Desc from admin.TBL_CODE_LOOKUP Where Code_Desc = @codedesc AND Code_Type = @type";
            DataTable dt = dbhelp.ExecDataReader(commandtext, "@codedesc", IDVal.Trim(), "@type", type.Trim());

            if (dt.Rows.Count > 0)
            {
                result = 0;
            }

            else
            {
                result = 1;
            }

            return result;

        }

        //To edit selected Code data based on the current Code Description and Code Type in Maintainence_Add.aspx
        //Parameter of current Code Description is oldcodedesc and current Code Type is type
        public int EditCode(string lookup, string codedesc, string oldcodedesc, string type)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();

            string commandtext = "UPDATE admin.TBL_CODE_LOOKUP SET Code = @lookup, Code_Desc = @codedesc WHERE Code_Type = @type AND CodeDesc = @oldcodedesc;";

            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@lookup", lookup, "@codedesc", codedesc, "@type", type, "@oldcodedesc", oldcodedesc);

            transcommand.Add(mycmd);

            result = dbhelp.ExecTrans(transcommand);

            return result;
        }

        //To delete Code data based on the Code Type and Code Description in Maintainence_Management.aspx
        //Parameter of Code Type is type and Code Description is codedesc
        public int DeleteCode(string type, string codedesc)
        {
            int result = 0;
            List<SqlCommand> transcommand = new List<SqlCommand>();
            SqlCommand mycmd = new SqlCommand();

            string commandtext = "DELETE FROM admin.TBL_CODE_LOOKUP WHERE Code_Type = @type AND Code_Desc = @codedesc;";

            mycmd = dbhelp.CreateCommand(commandtext, CommandType.Text, "@type", type, "@codedesc", codedesc);

            transcommand.Add(mycmd);

            result = dbhelp.ExecTrans(transcommand);

            return result;
        }

    }
}


//END OF CODE MANAGEMENT METHODS