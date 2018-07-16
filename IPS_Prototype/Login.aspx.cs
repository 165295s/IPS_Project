using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPS_Prototype.DAL;

using IPS_Prototype.Class;
using IPS_Prototype.RetrieveClass;



namespace IPS_Prototype
{
    public partial class Login_test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Login_Alert.Attributes.CssStyle.Remove("display");
            Login_Alert.Attributes.CssStyle.Add("display", "none");
        }

        
            protected void Login_Click(object sender, EventArgs e)
            {
            try
            {
                DatabaseDAO userObj = new DatabaseDAO();
                UserAddInfo user = new UserAddInfo();


                // Get user Roles, Name and Email to store in session of MasterPage and
                user = userObj.GetData(User_Login.Value);
                string role = user.Role;

                string Name = user.Name;
                string Email = user.Email;
                if (role != null || Name != null || Email != null)
                {

                    // If User information correct, let user login
                    if (role.Length > 1)
                    {

                        //Store role in session
                        Session.Add("role", role);
                        Session.Add("name", Name);
                        Session.Add("email", Email);

                        // After user successfully login
                        // Rirect to index2.aspx
                        Response.Redirect("User_Management.aspx");

                    }
                    else
                    {
                        // If User Information Wrong, display error message
                        Login_Alert.Attributes.CssStyle.Add("display", "block");

                    }
                }
                else
                {
                    // If system error, display error message through alert box
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('There seems to be a problem with the system! Contact the local Administrator');", true);
                }
            }
            catch(Exception ex)
            {
                //Catch all errors and write to ErrorLog.txt
                ErrorLog.WriteErrorLog(ex.ToString());
            }
                
            
        }
       
    }
}