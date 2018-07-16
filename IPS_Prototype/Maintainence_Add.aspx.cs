using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPS_Prototype.DAL;

namespace IPS_Prototype
{
    public partial class Maintainence_Add : System.Web.UI.Page
    {
        string codetype;
        string lookup;
        string codedescription;
        DatabaseDAO dao = new DatabaseDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Session["codedesc"] != null)
                {
                    //If session "codedesc" is not equals null, means edit button event was triggered from Maintainence_Management.aspx table
                    codedescription = Session["codedesc"].ToString();
                    codetype = Session["type"].ToString();
                    lookup = Session["lookup"].ToString();

                    //Change title of page to Edit
                    title.InnerText = "Code Maintainence > Edit Code";
                    CommonHeaderTitle.InnerText = "Edit";

                    //Sets the textbox values to the values stored in session
                    HeaderName.InnerText = codetype;
                    LkUpCode.Value = lookup;
                    CodeDesc.Value = codedescription;
                }
            }

            //If session "codedesc" is equals to null, means add button event was triggered from Maintainence_Management.aspx
            //Sets type textbox to session "type" value
            type.Value = Session["type"].ToString();
            HeaderName.InnerText = Session["type"].ToString();
        }

        protected void SubmitCode(object sender, EventArgs e)
        {
            if(Session["codedesc"] != null)
            {
                //If session "codedesc" is not equals null, means edit button event was triggered from Maintainence_Management.aspx table
                int check;

                //Trigger the EditCode Method in DatabaseDAO and store textbox values into parameters
                check = dao.EditCode(LkUpCode.Value.ToUpper(), CodeDesc.Value.ToUpper(), Session["codedesc"].ToString(), Session["type"].ToString());
                if (check == 1)
                {
                    //If no error, display success message
                    //Add parameter to change message of AlertBox
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Edited New Code: " + LkUpCode.Value.ToUpper() + "');", true);
                }
                else
                {
                    //If error, display failure message
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displayFailure();", true);
                }
            }
            else
            {
                //If session "codedesc" is equals to null, means add button event was triggered from Maintainence_Management.aspx
                int check;

                //Trigger the AddCode Method in DatabaseDAO and sotre textbox values into parameters
                check = dao.AddCode(Session["type"].ToString(), LkUpCode.Value.ToUpper(), CodeDesc.Value.ToUpper());
                if (check == 1)
                {
                    //If no error, display success message
                    //Add parameter to change message of AlertBox
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Code: "  + LkUpCode.Value.ToUpper() + "');", true);
                }
                else
                {
                    //If error, display failure message
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displayFailure();", true);
                }
            }
            
        }


        //Ajax Method to check if there is existing Code in database
        //Links to Ajax javascript in IPS_Vertical.js
        [System.Web.Services.WebMethod(EnableSession = true)]
        
        public static int checkLookUp(string IDVal, string type)
        {
            int check = 0;
            DatabaseDAO dao = new DatabaseDAO();
            check = dao.checkLookUpValidity(IDVal, type);
            return check;
        }


        //Ajax method to check if there is existing Code Description in database
        //Links to Ajax javascript in IPS_Vertical.js
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static int checkCodeDesc(string IDVal, string type)
        {
            int check = 0;
            DatabaseDAO dao = new DatabaseDAO();
            check = dao.checkCodeDescValidity(IDVal, type);
            return check;
        }
    }
}