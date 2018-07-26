using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Web.DynamicData;
using IPS_Prototype.DAL;
using IPS_Prototype.Class;

namespace IPS_Prototype.DynamicData.FieldTemplates
{
    public partial class AddPa_Modal : System.Web.DynamicData.FieldTemplateUserControl
    {
        private ArrayList pList;
        private ArrayList detailList;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            DatabaseDAO d1 = new DatabaseDAO();
            DataTable DT = new DataTable();
            DT = d1.GetLookupSearch("HONOURIFIC");
            ddlList.DataSource = DT;
            ddlList.DataTextField = "Code_Desc";
            ddlList.DataValueField = "Code"; //When insert, this value
            ddlList.DataBind();


            pList = (ArrayList)Session["indvPerson"];


            // Below 3 lines do not work 
            //detailList = (ArrayList)Session["CAREP_Details"];
            //associateType.InnerText = detailList[0].ToString();
            //bindPAtable();

            //if (IsPostBack)
            //{
            //    associateType.InnerText = pList[3].ToString();

            //}
        }

        protected void Submit_PA(object sender, EventArgs e)
        {
            int check = 0;
            try {
                MembershipDAO user_PA = new MembershipDAO();
                //check = user_PA.AddPA(ddlList.SelectedValue.ToString(), txtFirstName.Value, txtSurname.Value, txtTelephone.Value, txtEmail.Value);

                if (check == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New PA: " + txtSurname.Value+" "+txtFirstName.Value + "');", true);
                 

                }
                else if (check == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }


            }
            catch (Exception ex) {
                ErrorLog.WriteErrorLog(ex.ToString());
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);

            }


        }






    }
}
