using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPS_Prototype.DAL;
using IPS_Prototype.Class;
using System.Data.SqlClient;
using System.Data;
using IPS_Prototype.RetrieveClass;

namespace IPS_Prototype
{
    public partial class User_Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                string UserID = (string)(Session["UserID"]);
                if (UserID == null)
                {
                    //If session "UserID" is equals null, means edit button event was triggered from User_Management.aspx table
                   
                    title.InnerText = "User Management > Create User";
                    UserRegisterHeader.InnerText = "Create Account Details";
                    User_Input_Name.Value = "";
                    User_Input_Email.Value = "";
                    Select_Permission_Level.Value = "";
                    User_Input_Username.Value = "";

                }
                else
                {
                    
                    DatabaseDAO dao = new DatabaseDAO();
                    UserAddInfo user = new UserAddInfo();

                    //Get User data from GetData Method in DatabaseDAO
                    user = dao.GetData(UserID);
                    if (user.Name != null)
                    {
                        //If retrieved data "user.Name" is not equals null, means edit button event was triggered from User_Management.aspx table
                        //Change alert message text to updated
                        

                        //Change title of page to Edit User
                        title.InnerText = "User Management > Edit User";
                        UserRegisterHeader.InnerText = "Update Account Details";

                        //Sets the textbox values to the values retrieved from GetData Method in DatabaseDAO
                        User_Input_Username.Value = UserID;
                        User_Input_Email.Value = user.Email;
                        Select_Permission_Level.Value = user.Role;
                        User_Input_Name.Value = user.Name;
                    }
                    else
                    {
                        //If error, display failure message
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                    }

                }
            }
           
            
        }
        protected void Submit_User(object sender, EventArgs e)
        {
            if ((string)(Session["UserID"]) == null)
            {
                int check = 0;
                try
                {


                    DatabaseDAO user = new DatabaseDAO();
                    check = user.AddUser(User_Input_Username.Value, User_Input_Name.Value, User_Input_Email.Value, DateTime.Now, Select_Permission_Level.Value);


                    if (check == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Account: " + User_Input_Name.Value + " With Permission Level Of: " + Select_Permission_Level.Value + "');", true);
                    }
                    else if (check == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure('There seems to be an error! Please notify the Administrators.');", true);
                    }

                    User_Input_Name.Value = "";

                    User_Input_Email.Value = "";
                    Select_Permission_Level.SelectedIndex = 0;
                    User_Input_Username.Value = "";



                }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorLog(ex.ToString());
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }

            }
            else
            {
                int check = 0;
                try
                {
                    string UserID = (string)(Session["UserID"]);
                    DatabaseDAO user = new DatabaseDAO();
                    check = user.EditUser(User_Input_Name.Value, User_Input_Email.Value, Select_Permission_Level.Value, User_Input_Username.Value, UserID, DateTime.Now);
                    if (check == 1)
                    {
                       

                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Updated Account:" + User_Input_Name.Value +"');", true);
                    }
                    else if (check == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                    }
                    
                    
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorLog(ex.ToString());
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static int checkUserName(string IDVal)
        {
            int check = 0;
            DatabaseDAO dao = new DatabaseDAO();
            check = dao.checkIDValidity(IDVal);
            return check;
        }


    }
}